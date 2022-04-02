<template>
    <modal :dialog="dialog" :title="title" @close="ModalClose" @done="ModalDone" :width="700" transition="scale-transition" origin="center center">
        <v-form ref="form" :lazy-validation="true">
            <v-container>
                <v-row>
                    <v-col cols="12" md="6">
                        <v-select :no-data-text="$locales.t('noData')"
                        :items="countries" :rules="validationRules.required" :label="$locales.t('country')" 
                        item-text="name" item-value="id" return-object
                        outlined prepend-icon="fas fa-globe-africa" 
                        v-model="lookups.country"/>
                    </v-col>

                    <v-col cols="12" md="6">
                        <v-select :no-data-text="$locales.t('noData')"
                        :items="educationTypes" :rules="validationRules.required" :label="$locales.t('educationType')" 
                        item-text="name" item-value="id"
                        outlined prepend-icon="fas fa-school" 
                        v-model="lookups.educationType"/>
                    </v-col>

                    <v-col cols="12" md="6">
                        <v-select :no-data-text="$locales.t('noData')"
                        :items="educationYears" :rules="validationRules.required" :label="$locales.t('educationYear')"
                        item-text="name" item-value="id" return-object
                        outlined prepend-icon="fas fa-graduation-cap" 
                        v-model="lookups.educationYear"/>
                    </v-col>

                    <v-col cols="12" md="6">
                        <v-select :no-data-text="$locales.t('noData')"
                        :items="subjects" :rules="validationRules.required" :label="$locales.t('subject')" 
                        item-text="name" item-value="id" return-object
                        outlined prepend-icon="fas fa-book"
                        v-model="lookups.subject"/>
                    </v-col>

                    <v-col cols="12">
                        <v-select :no-data-text="$locales.t('noData')"
                        :items="$locales.t('days')" :rules="validationRules.required" 
                        :label="$locales.t('day')" 
                        item-text="name" item-value="id"
                        outlined prepend-icon="fas fa-calendar-day"
                        v-model="timetable.day"/>
                    </v-col>

                    <v-col cols="12" md="6">
                        <v-menu
                        v-model="startTime" :close-on-content-click="false" :nudge-right="40"
                        :return-value.sync="timetable.startTime" transition="scale-transition"
                        offset-y max-width="290px" min-width="290px" ref="startTime">
                            <template v-slot:activator="{ on }">
                                <v-text-field
                                outlined v-model="timetable.startTime" :rules="validationRules.required"
                                :label="$locales.t('startTime')" prepend-icon="fas fa-clock" readonly v-on="on" />
                            </template>
                            <v-time-picker
                            v-if="startTime"
                            v-model="timetable.startTime"
                            full-width
                            @click:minute="$refs.startTime.save(timetable.startTime)" />
                        </v-menu>
                    </v-col>

                    <v-col cols="12" md="6">
                        <v-menu
                        v-model="endTime" :close-on-content-click="false" :nudge-right="40"
                        :return-value.sync="timetable.endTime" transition="scale-transition"
                        offset-y max-width="290px" min-width="290px" ref="endTime">
                            <template v-slot:activator="{ on }">
                                <v-text-field
                                outlined v-model="timetable.endTime" :rules="validationRules.required"
                                :label="$locales.t('endTime')" prepend-icon="fas fa-clock" readonly v-on="on" />
                            </template>
                            <v-time-picker
                            v-if="endTime"
                            v-model="timetable.endTime"
                            full-width
                            @click:minute="$refs.endTime.save(timetable.endTime)" />
                        </v-menu>
                    </v-col>
                </v-row>
            </v-container>
        </v-form>
    </modal>
</template>

<script>
import Modal from '@/components/shared/modal.vue'
import ValidationMixin from '@/mixins/validate.js'
import LookupsMixin from '@/mixins/lookups.js'
import moment from 'moment'

export default {
    name: "SaveTimetableModal",

    components: {
        Modal
    },

    props: ['dialog'],

    mixins: [ValidationMixin, LookupsMixin],

    data: () => ({
        timetable: {},
        startTime: false,
        endTime: false,
        isEdit: false,
        title: ''
    }),
    
    mounted(){
        this.$eventBus.$on('modal-open', this.InitModal)
    },

    methods: {
        InitModal(oldEvent) {
            if(oldEvent){
                this.title = this.$locales.t('timetableModal.editTitle');
                this.isEdit = true;
                this.timetable = oldEvent;
                this.timetable.day = (oldEvent.day + 6) % 7 + 1;
                this.lookups.country = oldEvent.country;
                this.lookups.educationType = oldEvent.educationType
                this.lookups.educationYear = oldEvent.educationYear
                this.lookups.subject = oldEvent.course
            }
            else{
                this.lookups.country = null
                this.lookups.educationType = null
                this.lookups.educationYear = null
                this.lookups.subject = null
                this.timetable = {}
                this.title = this.$locales.t('timetableModal.editTitle')
                this.isEdit = false;
            }
            if(this.$refs.form)
                this.$refs.form.resetValidation()
        },
        ModalClose(){
            this.$emit("close");
            this.timetable = {}
            this.$refs.form.resetValidation()
        },
        ModalDone(){
            this.HttpRequest({
                url: 'api/timetable',
                method: this.isEdit ? 'PUT' : 'POST',
                data: {
                    day: this.timetable.day - 1,
                    startTime: moment(this.timetable.startTime, "HH:mm").utc().format("HH:mm"),
                    endTime: moment(this.timetable.endTime, "HH:mm").utc().format("HH:mm"),
                    CourseId: this.lookups.subject.id,
                    id: this.timetable.id
                },
                auth: true,
                successNotify: true,
            }).then(this.SaveDone)
            .catch(this.SaveFaild);
        },
        SaveDone(response){
            this.$emit('done', this.GenerateEvent());
            this.timetable = {}
            this.$refs.form.resetValidation()
        },
        SaveFaild(error){
            this.$emit('close');
            this.timetable = {}
            this.$refs.form.resetValidation()
        },
        GenerateEvent(){
            var event = {
                id: this.timetable.id,
                name: this.lookups.subject.name,
                start: '',
                end: '',
                day: this.timetable.day % 7,
                startTime: this.timetable.startTime,
                endTime: this.timetable.endTime,
                country: this.lookups.country,
                educationType: this.lookups.educationType,
                educationYear: this.lookups.educationYear,
                subject: this.lookups.subject,
            };

            var firstDay = moment().subtract((moment().day() + 1) % 7, 'days').startOf('day');
            var day = this.timetable.day % 7;
            firstDay.add(day, 'd');
            event.start = moment(`${firstDay.format("YYYY-MM-DD")} ${event.startTime}`)
            .format("YYYY-MM-DD HH:mm");
            event.end = moment(`${firstDay.format("YYYY-MM-DD")} ${event.endTime}`)
            .format("YYYY-MM-DD HH:mm");

            return event;
        }
    }
}
</script>

<style scoped>

</style>