<template>
    <v-form class="justify-center" ref="form" lazy-validation >
        
        <v-container grid-list-md>
            <v-row>
                <v-col cols="12">
                    <v-select v-model="userDetails.type"
                    :no-data-text="$locales.t('noData')"
                    item-text="name" outlined item-value="id"
                    :items="$locales.t('registerationRoles')" 
                    :rules="validationRules.required" 
                    :label="$locales.t('accountType')" 
                    required />
                </v-col>

                <v-col cols="12" sm="6">
                    <v-select v-model="lookups.country" 
                    :no-data-text="$locales.t('noData')"
                    :items="countries" item-value="id" item-text="name"
                    :rules="validationRules.required" outlined
                    :label="$locales.t('country')"
                    required />
                </v-col>

                <v-col cols="12" sm="6">
                    <v-select v-model="lookups.educationType" 
                    :no-data-text="$locales.t('noData')"
                    :items="educationTypes" item-value="id" item-text="name" 
                    :rules="validationRules.required" outlined
                    :label="$locales.t('educationType')" 
                    required />
                </v-col>

                <v-col v-if="userDetails.type == 1" cols="12">
                    <v-select v-model="lookups.countrySubject" 
                    :no-data-text="$locales.t('noData')"
                    :items="countrySubjects" item-value="id" item-text="name" 
                    :rules="validationRules.required" outlined
                    :label="$locales.t('subject')" 
                    required />
                </v-col>

                <v-col cols="12" v-if="userDetails.type == 2">
                    <v-select v-model="lookups.educationYear" 
                    :no-data-text="$locales.t('noData')"
                    :items="educationYears" item-value="id" item-text="name" 
                    :rules="validationRules.required" outlined
                    :label="$locales.t('educationYear')" 
                    required />
                </v-col>
            </v-row>
        </v-container>

        <v-text-field
        v-if="userDetails.type == 2"
        outlined
        v-model="userDetails.student.parentPhoneNumber"
        :rules="validationRules.phoneNumber"
        :label="$locales.t('parentPhoneNumber')"
        prepend-icon="fas fa-phone"
        required />

        <v-checkbox
        class="rules-checkbox"
        :rules="validationRules.required"
        :label="$locales.t('termsAndConditions')"
        color="primary"
        value="true"
        hide-details />
        
    </v-form>
</template>

<script>
import validateMixin from '@/mixins/validate.js'
import LookupsMixin from '@/mixins/lookups.js'

export default {
    name: "SignupStepThree",
    
    mixins: [validateMixin, LookupsMixin],
    
    data: () => ({
        userDetails: {}
    }),

    mounted(){
        this.$refs.form.resetValidation()
    },

    methods: {
        validate(){
            return this.$refs.form.validate();
        }
    }
}
</script>

<style>

</style>