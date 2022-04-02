<template>
    <div class="signup-page">
        <default-header />
        <signup-form />
        <default-footer />
    </div>
</template>

<script>
import DefaultHeader from '@/components/layout/default-header.vue'
import SignupForm from '@/components/forms/signup-form'
import DefaultFooter from '@/components/layout/default-footer.vue'

export default {
    name: "Signup",

    components: {
        DefaultHeader,
        SignupForm,
        DefaultFooter
    }
}
</script>

<style scoped>

</style>