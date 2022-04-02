<template>
    <v-container fluid class="text-center">
        <v-row no-gutters>
            <v-col order-lg="1" order="2" lg="8" cols="12" class="my-6">
                <account-setting-form :role="role" />
            </v-col>

            <v-col order-lg="2" order="1" lg="4" cols="12" class="my-6">
                <account-card isProfileCard :role="role" class="ma-2"/>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
import AccountSettingForm from '@/components/forms/account-setting-form.vue'
import AccountCard from '@/components/cards/account-card.vue'

export default {
    name: "ProfileAccountSettings",

    components: {
        AccountSettingForm,
        AccountCard
    },

    computed: {
        role(){
            var lang = this.$locales.current;
            var roles = this.$route.path.toLowerCase().split("/");
            return this.$locales.t(roles[2]);
        }
    },
}
</script>

<style scoped>

</style>