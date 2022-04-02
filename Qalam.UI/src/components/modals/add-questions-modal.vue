<template>
    <modal :dialog="dialog" title="اضافة اسئلة جديدة" @close="ModalClose" @done="ModalDone" :width="700">
        <v-form ref="form" :lazy-validation="true">
            <v-container>
                <v-row no-gutters>
                    <v-col cols="12" md="6">
                        <v-select :no-data-text="$locales.t('noData')" 
                            :items="[]" :rules="validationRules.required" :label="$locales.t('country')" 
                            outlined prepend-icon="fas fa-globe-africa" class="ma-1" 
                            v-model="questions.countryId"/>
                    </v-col>
                    <v-col cols="12" md="6">
                        <v-select :no-data-text="$locales.t('noData')"
                        :items="[]" :rules="validationRules.required" :label="$locales.t('educationType')" 
                        outlined prepend-icon="fas fa-school" class="ma-1" 
                        v-model="questions.educationTypeId"/>
                    </v-col>
                    <v-col cols="12" md="6">
                        <v-select :no-data-text="$locales.t('noData')" 
                        :items="[]" :rules="validationRules.required" :label="$locales.t('educationYear')" 
                        outlined prepend-icon="fas fa-graduation-cap" class="ma-1" 
                        v-model="questions.educationYearId"/>
                    </v-col>
                    <v-col cols="12" md="6">
                        <v-select :no-data-text="$locales.t('noData')" 
                        :items="[]" :rules="validationRules.required" :label="$locales.t('semester')" 
                        outlined prepend-icon="fas fa-calendar-day" class="ma-1" 
                        v-model="questions.semesterId"/>
                    </v-col>
                    <v-col cols="12" md="6">
                        <v-select :no-data-text="$locales.t('noData')"
                        :items="[]" :rules="validationRules.required" :label="$locales.t('subject')" 
                        outlined prepend-icon="fas fa-book" class="ma-1" 
                        v-model="questions.subjectId"/>
                    </v-col>
                    <v-col cols="12" md="6">
                        <v-select :no-data-text="$locales.t('noData')"
                        :items="[]" :rules="validationRules.required" :label="$locales.t('questionType')" 
                        outlined prepend-icon="fas fa-question" class="ma-1" 
                        v-model="questions.questionTypeId"/>
                    </v-col>
                    <v-col cols="12">
                        <v-file-input
                            outlined
                            accept=".csv"
                            :label="$locales.t('questionFile')"
                            v-model="questions.file"
                            :rules="validationRules.required" />
                    </v-col>
                </v-row>
            </v-container>
        </v-form>
    </modal>
</template>

<script>
import Modal from '@/components/shared/modal.vue'
import ValidationMixin from '@/mixins/validate.js'

export default {
    name: "AddQuestionsModal",

    components: {
        Modal
    },

    props: ['dialog'],

    mixins: [ValidationMixin],

    data: () => ({
        questions: {
            countryId: 0,
            educationTypeId: 0,
            educationYearId: 0,
            semesterId: 0,
            subjectId: 0,
            questionTypeId: 0,
            file: null
        }
    }),

    updated() {
        this.$refs['form'].resetValidation();
    },

    methods: {
        ModalClose(){
            this.$emit("close");
        },
        ModalDone(){
            if(this.$refs['form'].validate()){
                this.$emit("done");
            }
        }
    }
}
</script>

<style>

</style>