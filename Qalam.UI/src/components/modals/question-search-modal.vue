<template>
    <modal :dialog="dialog" title="البحث في الأسئلة" @close="ModalClose" @done="ModalDone" :width="700">
        <v-form ref="form" :lazy-validation="true">
            <v-container autofocus>
                <v-row no-gutters>
                    <v-col cols="12" md="6">
                        <v-select :no-data-text="$locales.t('noData')"
                        :items="[]" :rules="validationRules.required" label="الدولة" 
                        outlined prepend-icon="fas fa-globe-africa" class="ma-1" 
                        v-model="filters.countryId"/>
                    </v-col>
                    <v-col cols="12" md="6">
                        <v-select :no-data-text="$locales.t('noData')"
                        :items="[]" :rules="validationRules.required" label="النظام التعليمي" 
                        outlined prepend-icon="fas fa-school" class="ma-1" 
                        v-model="filters.educationTypeId"/>
                    </v-col>
                    <v-col cols="12" md="6">
                        <v-select :no-data-text="$locales.t('noData')"
                        :items="[]" :rules="validationRules.required" label="السنة الدراسية" 
                        outlined prepend-icon="fas fa-graduation-cap" class="ma-1" 
                        v-model="filters.educationYearId"/>
                    </v-col>
                    <v-col cols="12" md="6">
                        <v-select :no-data-text="$locales.t('noData')"
                        :items="[]" :rules="validationRules.required" label="الفصل الدراسي" 
                        outlined prepend-icon="fas fa-calendar-day" class="ma-1" 
                        v-model="filters.semesterId"/>
                    </v-col>
                    <v-col cols="12" md="6">
                        <v-select :no-data-text="$locales.t('noData')"
                        :items="[]" :rules="validationRules.required" label="المادة" 
                        outlined prepend-icon="fas fa-book" class="ma-1" 
                        v-model="filters.subjectId"/>
                    </v-col>
                    <v-col cols="12" md="6">
                        <v-select :no-data-text="$locales.t('noData')"
                        :items="[]" :rules="validationRules.required" label="نوع الأسئلة" 
                        outlined prepend-icon="fas fa-question" class="ma-1" 
                        v-model="filters.questionTypeId"/>
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
    name: "QuestionSearchModal",

    components: {
        Modal
    },

    mixins: [ValidationMixin],

    props: {
        dialog: {
            type: Boolean,
            required: true
        }
    },

    data: () => ({
        filters: {
            countryId: 0,
            educationTypeId: 0,
            educationYearId: 0,
            semesterId: 0,
            subjectId: 0,
            questionTypeId: 0
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