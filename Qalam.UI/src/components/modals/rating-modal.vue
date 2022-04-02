<template>
    <modal :dialog="dialog" :title="$locales.t('ratingModal.title')" @close="ModalClose" @done="ModalDone" width="500">
        <v-form ref="form" lazy-validation>
            <v-container>
                <v-row class="text-center">
                    <v-col cols="12">
                        <span class="title">{{$locales.t('ratingModal.text')}}</span>
                    </v-col>
                    <v-col cols="12" class="mt-5">
                        <v-rating v-model="review.rating" color="yellow darken-3"
                        background-color="grey darken-1" empty-icon="fas fa-star"
                        clearable full-icon="fas fa-star" hover />
                    </v-col>
                    <v-col cols="12" class="mt-5">
                        <v-textarea v-model="review.comment" outlined
                        no-resize :label="$locales.t('ratingModal.opinionTextField')" />
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
    name: "RatingModal",

    components: {
        Modal
    },

    mixins: [ValidationMixin],

    props: ['dialog'],

    data: () => ({
        review: {
            rating: 0,
            comment: ''
        }
    }),
    
    methods: {
        ModalClose(){
            this.$emit("close");
        },

        ModalDone(){
            this.HttpRequest({
                url: '',
                Method: 'POST',
                data: null
            }).then(this.PostSuccess)
            .catch(this.PostFaild);
        },
        PostSuccess(response){
            this.$emit("done");
        },
        PostFaild(error){
            // error
            this.$emit("done");
        }
    }
}
</script>

<style scoped>

</style>