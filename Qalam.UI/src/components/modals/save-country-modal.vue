<template>
    <modal :dialog="dialog" title="اضافة دولة" :showBtn="false" @close="ModalClose" @done="ModalDone" transition="dialog-bottom-transition" origin="">
        <country-form @done="ModalDone" @close="ModalClose" :old-country="oldCountry" />
    </modal>
</template>

<script>
import Modal from '@/components/shared/modal.vue'
import CountryForm from '@/components/forms/country-form.vue'

export default {
    name: "SaveCountryModal",

    components: {
        Modal,
        CountryForm
    },

    props: ['dialog', 'old-country'],

    methods: {
        ModalClose(){
            this.$emit("close");
        },

        ModalDone(country){
            this.$emit("done", country);
        }
    }
}
</script>

<style scoped>
</style>