<template>
    <v-form class="justify-center" ref="form" lazy-validation >
        
        <v-text-field
        class="mt-2"
        outlined
        v-model="userDetails.userName"
        :counter="32"
        :rules="validationRules.name"
        :label="$locales.t('userName')"
        prepend-icon="fas fa-user"
        required />

        <v-text-field
        outlined
        v-model="userDetails.email"
        :rules="validationRules.email"
        :label="$locales.t('email')"
        prepend-icon="fas fa-envelope"
        required />

        <v-text-field
        outlined
        v-model="userDetails.password"
        :rules="validationRules.password"
        :label="$locales.t('password')"
        :append-icon="showPassword ? 'far fa-eye' : 'far fa-eye-slash'"
        :type="showPassword ? 'text' : 'password'"
        @click:append="showPassword = !showPassword"
        prepend-icon="fas fa-lock"
        required />

        <v-text-field
        outlined
        v-model="userDetails.repeatPassword"
        :rules="ValidateMatchPassword(userDetails.password)"
        :label="$locales.t('repeatPassword')"
        :append-icon="showRePassword ? 'far fa-eye' : 'far fa-eye-slash'"
        :type="showRePassword ? 'text' : 'password'"
        @click:append="showRePassword = !showRePassword"
        prepend-icon="fas fa-lock"
        required />

    </v-form>
</template>

<script>
import validateMixin from '@/mixins/validate.js'

export default {
    name: "SignupStepTwo",

    mixins: [validateMixin],

    data: () => ({
        userDetails: {},
        showPassword: false,
        showRePassword: false,
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