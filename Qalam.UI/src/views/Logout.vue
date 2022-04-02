<template>
    <warning-sign icon="fas fa-door-open">
        <span v-html="$locales.t('logoutMessage')" />
    </warning-sign>
</template>

<script>
import WarningSign from '@/components/shared/warning-sign.vue'

export default {
    name: "Logout",

    components: {
        WarningSign
    },

    mounted(){
        setTimeout((() => {
            location.href = '/';
        }).bind(this), 5000);
        this.$store.commit("UpdateUserData", {upToDate: false});
        this.$qalamHub.$emit("disconnect");
        this.Logout();
    },

    methods: {
        
    }
}
</script>

<style>

</style>