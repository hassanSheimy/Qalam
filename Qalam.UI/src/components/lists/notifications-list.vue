<template>
    <v-list three-line>
        <empty-state v-if="notifications.length == 0" />
        <template v-for="(notification, index) in notifications">
            <v-list-item :key="'notification-' + index" :to="'/live/watch/o9YH5Va0W'">
                <v-list-item-avatar>
                    <v-img :src="notification.sender.image || defaultImage"></v-img>
                </v-list-item-avatar>

                <v-list-item-content>
                    <v-list-item-title>
                        <span class="subtitle-1">{{notification.message}}</span>
                    </v-list-item-title>
                    <v-list-item-subtitle>
                        <span class="subtitle-2">
                            {{notification.sender.fullName}}
                        </span>
                    </v-list-item-subtitle>
                </v-list-item-content>
            </v-list-item>
            <v-divider :key="'divider-' + index" inset />
        </template>
    </v-list>
</template>

<script>
import EmptyState from '@/components/shared/empty-state.vue'
import defaultImage from '@/assets/images/user.jpg'

export default {
    name: "NotificationsList",

    components: {
        EmptyState
    },

    props: {
        notifications: {
            type: Array,
            required: true
        }
    },

    data: () => ({
        defaultImage
    }),

    mounted() {
        console.log(this.notifications);
    },

    methods: {
        url(index){
            return this.notifications[index].url;
        }
    },
}
</script>

<style scoped>

</style>