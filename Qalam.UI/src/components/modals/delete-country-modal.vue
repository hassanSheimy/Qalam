<template>
    <modal :dialog="dialog" :title="$locales.t('countryModal.deleteTitle')" @close="ModalClose" @done="ModalDone" width="500">
        <v-form ref="form" lazy-validation>
            <v-container>
                <v-row no-gutters class="mt-7">
                    <v-col cols="12" class="text-center">
                        <p>{{$locales.t('countryModal.deleteText')}}</p>
                        <p class="error--text">{{$locales.t('countryModal.deleteNotes')}}</p>
                    </v-col>

                    <v-col cols="12">
                        <v-text-field outlined :label="$locales.t('confirmBtn')" 
                        :rules="ValidateMatch('Confirm')" v-model="text" />
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
    name: "DeleteCountryModal",

    components: {
        Modal
    },

    mixins: [ValidationMixin],

    props: ['dialog', 'id'],

    data: () => ({
        text: null,
    }),

    methods: {
        ModalReset(){
            this.text = null,
            this.$refs["form"].resetValidation();
        },
        ModalClose(){
            this.ModalReset()
            this.$emit("close");
        },
        ModalDone(){
            this.HttpRequest({
                url: 'api/countries/' + this.id,
                method: 'DELETE',
                auth: true,
                successNotify: true
            }).then(this.DeleteSuccess)
            .catch(this.DeleteFaild);
        },
        DeleteSuccess(response){
            // deleted successfully
            this.ModalReset()
            this.$emit("done");
        },
        DeleteFaild(error){
            // error happened
            this.ModalClose();
        }
    }
}
</script>

<style scoped>

</style>