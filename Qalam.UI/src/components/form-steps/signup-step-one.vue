<template>
    <v-form class="justify-center" ref="form" :lazy-validation="true" >
        <v-text-field
        class="mt-2"
        outlined
        v-model="userDetails.firstName"
        :counter="32"
        :rules="validationRules.name"
        :label="$locales.t('firstName')"
        prepend-icon="fas fa-user"
        required />

        <v-text-field
        outlined
        v-model="userDetails.lastName"
        :counter="32"
        :rules="validationRules.name"
        :label="$locales.t('lastName')"
        prepend-icon="fas fa-user"
        required />

        <v-text-field
        outlined
        v-model="userDetails.phoneNumber"
        :rules="validationRules.phoneNumber"
        :label="$locales.t('phoneNumber')"
        prepend-icon="fas fa-phone"
        required />

        <v-menu
            v-model="date"
            :close-on-content-click="false"
            max-width="290">
        <template v-slot:activator="{ on }">
            <v-text-field
            v-model="userDetails.birthDate"
            outlined
            :label="$locales.t('bairthDay')"
            prepend-icon="fas fa-birthday-cake"
            :rules="validationRules.date"
            v-on="on"
            readonly
            @click:clear="date = null" />
        </template>
        <v-date-picker
            v-model="userDetails.birthDate"
            @change="date = false" />
        </v-menu>
        
    </v-form>
</template>

<script>
import validateMixin from '@/mixins/validate.js'

export default {
    name: "SignupStepOne",

    mixins: [validateMixin],

    data: () => ({
        userDetails: {},
        date: false
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