<template>
    <modal :dialog="dialog" :title="$locales.t('teacherYearModal.addTitle')" @close="ModalClose" @done="ModalDone" width="500">
        <v-form ref="form" lazy-validation class="mt-5">
            <v-container grid-list-md>
                <v-row no-gutters >
                    <v-col cols="12">
                        <v-select :no-data-text="$locales.t('noData')"
                            outlined item-text="name" item-value="id" v-model="lookups.educationYear"
                            prepend-icon="fas fa-school" :items="availableYears" return-object
                            :rules="validationRules.required" :label="$locales.t('educationYear')" required />
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

export default {
    name: "AddTeacherYearModal",

    components: {
        Modal
    },

    mixins: [ValidationMixin, LookupsMixin],

    props: ['dialog'],

    data: () => ({
        year: {
            id: 0,
            name: 0,
        },
        teacher: {}
    }),

    computed: {
        availableYears() {
            return this.educationYears.filter(y => y.courses.some(s => s.name == this.teacher.subjectName))
        }
    },

    created() {
        this.$eventBus.$on('init-add-year-modal', this.InitModal);
    },
    
    methods: {
        InitModal(){
            this.teacher = this.$store.getters.UserDetails;
            this.lookups.country = this.teacher.countryId;
            this.lookups.educationType = this.teacher.educationTypeId;
            
            if(this.$refs.form)
                this.$refs.form.resetValidation();
        },
        ModalClose(){
            this.$emit("close");
        },

        ModalDone(){
            var courseId = this.subjects.find(s => s.name == this.teacher.subjectName).id;
            this.HttpRequest({
                url: 'api/teacher/course',
                method: 'POST',
                data: {
                    courseId,
                },
                auth: true
            }).then(this.SaveDone)
            .catch(this.$emit("close"));
        },

        SaveDone(respose){
            this.year.id = respose.data;
            this.year.name = this.lookups.educationYear.name;
            this.$emit("done", this.year);
        }
    }
}
</script>