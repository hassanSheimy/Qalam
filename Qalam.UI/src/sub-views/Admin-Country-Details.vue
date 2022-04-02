<template>
    <warning-sign>
        <span v-html="$locales.t('notCompletedPage')" />
    </warning-sign>
</template>

<script>
import WarningSign from '@/components/shared/warning-sign.vue'

export default {
    name: "AdminCountryDetails",

    components: {
        WarningSign
    }
}
</script>

<style scoped>
</style>