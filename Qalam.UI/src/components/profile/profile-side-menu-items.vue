<template>
    <div>
        <v-list>
            <v-list-item two-line>
                <v-list-item-avatar>
                    <img :src="userDetails.imagePath || defaultImage">
                </v-list-item-avatar>

                <v-list-item-content>
                    <v-list-item-title class="white--text">{{userDetails.userName}}</v-list-item-title>
                    <v-list-item-subtitle class="white--text">{{UserRole}}</v-list-item-subtitle>
                </v-list-item-content>
            </v-list-item>

            <v-container>
                <v-row no-gutters justify="center">
                    <v-col cols="6">
                        <v-list-item @click="notificationDialog = true">
                            <v-list-item-icon>
                                <v-badge v-if="userDetails.notificationsCount > 0" overlap color="primary" :content="userDetails.notificationsCount"> 
                                    <v-icon class="white--text">fa fa-bell</v-icon>
                                </v-badge>
                                <v-icon v-else class="white--text">fa fa-bell</v-icon>
                            </v-list-item-icon>
                        </v-list-item>
                    </v-col>
                    <v-col cols="6">
                        <v-list-item>
                            <v-list-item-icon>
                                <v-icon @click="login = false" to="/logout" class="white--text">fa fa-power-off</v-icon>
                            </v-list-item-icon>
                        </v-list-item>
                    </v-col>
                </v-row>
            </v-container>

            <v-divider />
            <v-list-item v-for="(item, index) in items" :disabled="item.soon" :key="'profile-sidebar-item' + index" class="py-1 secondary--hover justify-start" :to="!item.soon ? item.to : ''" exact>
                <v-list-item-icon>
                    <v-badge v-if="item.soon" overlap color="primary" :content="$locales.t('soonBadge')"> 
                        <v-icon class="white--text">{{ item.icon }}</v-icon>
                    </v-badge>
                    <v-icon v-else class="white--text">{{ item.icon }}</v-icon>
                </v-list-item-icon>
                <v-list-item-content>
                    <v-list-item-title class="white--text subtitle-1">{{ item.title }}</v-list-item-title>
                </v-list-item-content>
            </v-list-item>
        </v-list>
        <notification-modal :dialog="notificationDialog" @close="notificationDialog = false" :notifications="userDetails.notifications" />
    </div>
</template>

<script>
import UserDefultImage from '@/assets/images/user.jpg'
import NotificationModal from '@/components/modals/notifications-modal.vue'

export default {
    name: "ProfileSideItems",

    components: {
        NotificationModal
    },

    props: {
        items: {
            type: Array,
            required: true
        },
    },

    data: () => ({
        userDetails: {
            userName: '',
            imagePath: '',
            notifications: []
        },
        defaultImage: UserDefultImage,
        notificationDialog: false
    }),

    created() {
        this.$eventBus.$on('user-details-updated', this.UpdateUser);
    },

    mounted() {
        this.UpdateUser();
    },

    methods: {
        UpdateUser(){
            this.userDetails.userName = this.$store.getters.UserDetails.userName;
            this.userDetails.imagePath = this.$store.getters.UserDetails.imagePath;
            this.userDetails.notifications = this.$store.getters.UserDetails.notifications;
        },
    },

    computed: {
        UserRole(){
            var lang = this.$locales.current;
            var roles = this.$route.path.toLowerCase().split("/");
            return this.$locales.t(roles[2]);
        }
    },

    
}
</script>

<style scoped>

</style>