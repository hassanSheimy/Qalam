<template>
    <modal :dialog="dialog" :title="$locales.t('liveNameModal.title')" @close="ModalClose" @done="ModalDone" width="500">
        <v-form ref="form" lazy-validation>
            <v-container>
                <v-row>
                    <v-col cols="12">
                        <v-text-field :label="$locales.t('lesson')" 
                        :rules="validationRules.name" outlined :counter="32" required v-model="liveName" 
                        @keydown.enter="ModalClose" />
                        <v-text-field v-show="false" />
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
    name: "AddLiveStreamNameModal",

    components: {
        Modal
    },

    mixins: [ValidationMixin],

    props: ['dialog', 'live-stream'],

    data: () => ({
        liveName: ''
    }),
    
    methods: {
        ModalClose(){
            if(this.$refs.form.validate()){
                this.ModalDone();
            }
        },

        ModalDone(){
            this.HttpRequest({
                url: 'api/live',
                method: 'POST',
                data: {
                    name: this.liveName,
                    courseId: this.liveStream.courseId,
                    teacherId: this.liveStream.teacherId,
                    teacherTimetableId: this.liveStream.teacherTimetableId
                },
                auth: true
            }).then(this.PostSuccess)
            .catch(this.PostFaild);
        },
        PostSuccess(response){
            this.$emit("done", {
                name: this.liveName,
                id: response.data
            });
        },
        PostFaild(error){
            // error
            this.$emit("close");
        }
    }
}
</script>