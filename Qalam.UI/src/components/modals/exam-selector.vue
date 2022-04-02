<template>
    <modal :title="$locales.t('selectExamModal.title')" :btnText="$locales.t('startExamBtn')" :dialog="dialog" @close="ModalClose" @done="ModalDone">
        <v-form ref="form" v-model="valid" :lazy-validation="true">
            <v-container grid-list-md>
                <v-row>
                    <v-col cols="12" sm="6">
                        <v-select v-model="lookups.country" outlined
                            :no-data-text="$locales.t('noData')"
                            :items="countries" item-text="name" item-value="id"
                            :rules="validationRules.required" 
                            :label="$locales.t('country')"
                            prepend-icon="fas fa-globe-africa" />
                    </v-col>
                    <v-col cols="12" sm="6">
                        <v-select v-model="lookups.educationType"
                            outlined :no-data-text="$locales.t('noData')"
                            :items="educationTypes" item-text="name" item-value="id"
                            prepend-icon="fas fa-school"
                            :rules="validationRules.required" 
                            :label="$locales.t('educationType')" />
                    </v-col>
                    <v-col cols="12" sm="6">
                        <v-select v-model="lookups.educationYear" 
                            outlined :no-data-text="$locales.t('noData')"
                            :items="educationYears"
                            item-text="name" item-value="id" 
                            prepend-icon="fas fa-graduation-cap" 
                            :rules="validationRules.required" 
                            :label="$locales.t('educationYear')" />
                    </v-col>
                    <v-col cols="12" sm="6">
                        <v-select v-model="lookups.semester" 
                            outlined :no-data-text="$locales.t('noData')"
                            :items="semesters" 
                            item-text="name" item-value="id"
                            prepend-icon="fas fa-calendar-alt" 
                            :rules="validationRules.required" 
                            :label="$locales.t('semester')" />
                    </v-col>
                    <v-col cols="12" sm="6">
                        <v-select v-model="lookups.subject" 
                            outlined :no-data-text="$locales.t('noData')"
                            :items="subjects" 
                            item-text="name" item-value="id"
                            prepend-icon="fas fa-book" 
                            :rules="validationRules.required" 
                            :label="$locales.t('subject')" />
                    </v-col>
                    <v-col cols="12" sm="6">
                        <v-select v-model="lookups.lesson"
                            outlined :no-data-text="$locales.t('noData')"
                            :items="subjectLessons" 
                            item-text="name" item-value="id"
                            prepend-icon="fas fa-book-open"
                            :rules="validationRules.required" 
                            :label="$locales.t('lesson')"  
                            multiple />
                    </v-col>

                    <v-col cols="12">
                        <v-subheader class="pl-0">{{$locales.t('examDifficulty')}}</v-subheader>
                        <v-slider v-model="examDetails.difficulty" 
                            min="0" max="3" :color="color"
                            :tick-labels="$locales.t('difficultyLevels')" />
                    </v-col>
                </v-row>
            </v-container>
        </v-form>
    </modal>
</template>

<script>
import Modal from '@/components/shared/modal.vue'
import validateMixin from '@/mixins/validate.js'
import LookupsMixin from '@/mixins/lookups.js'

export default {
    name: "ExamSelector",

    components: {
        Modal
    },

    mixins: [validateMixin, LookupsMixin],

    props: ['dialog'],
    
    data: () => ({
        valid: true,
        select: '',
        examDetails: {
            country: 1,
            educationType: 1,
            educationYear: 1,
            semester: 1,
            subject: 1,
            lessons: [],
            difficulty: 0
        },
        colors: ['primary', 'warning', 'red accent-1', 'error']
        
    }),

    computed: {
        color () {
            return this.colors[this.examDetails.difficulty]
        },
    },

    methods: {
        ModalClose() {
            this.$emit("close");
        },
        ModalDone(){
            this.$emit("done");
        }
    }
}
</script>

<style>

</style>