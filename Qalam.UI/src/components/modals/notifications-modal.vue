<template>
    <modal :dialog="dialog" :title="$locales.t('notificationModal.title')" @close="ModalClose" 
    @done="ModalDone" width="700" :showBtn="false">

        <notifications-list :notifications="notifications" />

    </modal>
</template>

<script>
import Modal from '@/components/shared/modal.vue'
import NotificationsList from '@/components/lists/notifications-list.vue'

export default {
    name: "NotificationsModal",

    components: {
        Modal,
        NotificationsList
    },

    props: ['dialog', 'notifications'],
    
    methods: {
        ModalClose(){
            this.$emit("close");
        },
        ModalDone(){
            this.$emit("done");
        }
    }
    
}
</script>

<style scoped>

</style>