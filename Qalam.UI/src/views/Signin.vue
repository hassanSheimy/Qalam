<template>
    <div class="signin-page">
        <default-header />
        <signin-form />
        <default-footer />
    </div>
</template>

<script>
import DefaultHeader from '@/components/layout/default-header.vue'
import SigninForm from '@/components/forms/signin-form.vue'
import DefaultFooter from '@/components/layout/default-footer.vue'

export default {
    name: "Signin",

    components: {
        DefaultHeader,
        SigninForm,
        DefaultFooter
    }
}
</script>

<style scoped>

</style>