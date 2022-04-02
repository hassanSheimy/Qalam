<template>
    <modal :dialog="dialog" :title="$locales.t('timetableModal.deleteTitle')" @close="ModalClose" @done="ModalDone" width="500" transition="scale-transition" origin="center center">
        <p class="subtitle-1 mt-5">{{$locales.t('timetableModal.deleteText')}}</p>
    </modal>
</template>

<script>
import Modal from '@/components/shared/modal.vue'

export default {
    name: "DeleteTimetableModal",

    components: {
        Modal
    },

    props: ['dialog', 'id'],

    methods: {
        ModalClose(){
            this.$emit("close");
        },
        ModalDone(){
            this.HttpRequest({
                url: 'api/timetable/' + this.id,
                method: 'DELETE',
                auth: true,
                successNotify: true
            }).then(r => {
                this.$emit("done");
            }).catch(e => {
                this.$emit("close");
            });
        }
    }
}
</script>

<style scoped>

</style>