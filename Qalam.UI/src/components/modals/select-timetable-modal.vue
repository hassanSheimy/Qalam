<template>
    <modal :dialog="dialog" title="تحديد الموعد" @close="ModalClose" @done="ModalDone" width="500">
        <v-form ref="form" :lazy-validation="true" class="mt-5">
            <v-container>
                <v-row no-gutters>
                    <v-col cols="12">
                        <v-menu
                            v-model="startTimeBox" :close-on-content-click="false" :nudge-right="40"
                            :return-value.sync="timetable.startTime" transition="scale-transition"
                            offset-y max-width="290px" min-width="290px" ref="startTime">
                            <template v-slot:activator="{ on }">
                                <v-text-field
                                    outlined class="ma-1" v-model="timetable.startTime" :rules="ValidateValueBetween(startTime, endTime)"
                                    :label="$locales.t('startTime')" prepend-icon="fas fa-clock" readonly v-on="on" />
                            </template>
                            <v-time-picker
                                v-if="startTimeBox"
                                v-model="timetable.startTime"
                                full-width
                                @click:minute="$refs.startTime.save(timetable.startTime)" />
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
import moment from 'moment';

export default {
    name: "SelectTimetableModal",

    components: {
        Modal
    },

    mixins: [ValidationMixin],

    props: ['dialog', 'start-time', 'end-time', 'day', 'timetable-id'],

    data: () => ({
        timetable: {
            startTime: null,
            endTime: null,
        },
        startTimeBox: false
    }),

    mounted() {
    },
    
    methods: {
        ModalClose(){
            this.$emit("close");
        },

        ModalDone(){
            this.HttpRequest({
                url: 'api/teacher/timetable',
                method: 'POST',
                data: {
                    startTime: moment(this.timetable.startTime, "HH:mm").utc().format("HH:mm"),
                    id: this.timetableId
                },
                auth: true,
                successNotify: true
            }).then((r) => {})
            .catch((e) => {});
            this.$emit("done", this.GetFormatedTime());
        },
        GetFormatedTime(){
            this.timetable.endTime = moment(this.timetable.startTime, "HH:mm").add(1, 'hours').format("HH:mm");
            var firstDay = moment().subtract((moment().day() + 1) % 7, 'days').startOf('day');
            firstDay.add(this.day, 'd');
            var start = moment(`${firstDay.format("YYYY-MM-DD")} ${this.timetable.startTime}`)
            .format("YYYY-MM-DD HH:mm");
            var end = moment(`${firstDay.format("YYYY-MM-DD")} ${this.timetable.endTime}`)
            .format("YYYY-MM-DD HH:mm");
            
            return {
                start,
                end,
                startTime: this.timetable.startTime,
                endTime: this.timetable.endTime
            }
        }
    }
}
</script>