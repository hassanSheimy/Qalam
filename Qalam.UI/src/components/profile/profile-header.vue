<template>
    <div>
        <v-navigation-drawer v-model="sidebar" clipped 
        :temporary="temporary" :mini-variant="mini" 
        app overflow mini-variant-width="100" 
        :right="$vuetify.rtl" color="sidebar"
        :mobile-break-point="$vuetify.breakpoint.thresholds.sm">

            <profile-side-menu-items :items="items" />
        
        </v-navigation-drawer>

        <v-app-bar :clipped-right="$vuetify.rtl" :clipped-left="!$vuetify.rtl" app color="header" height="77">
            <router-link to="/" class="remove-underline">
                <v-toolbar-title class="primary--text app-arabic-font navbar-brand">{{$locales.t('appName')}}</v-toolbar-title>
            </router-link>
            <v-app-bar-nav-icon @click="ToggleNavbar" :class="$vuetify.rtl ? 'mr-auto' : 'ml-auto'"/>
        </v-app-bar>
    </div>
</template>

<script>
import ProfileSideMenuItems from './profile-side-menu-items.vue'

export default {
    name: "ProfileHeader",

    components: {
        ProfileSideMenuItems
    },

    props: {
        items: {
            type: Array,
            required: true
        },
    },

    data: () => ({
        sidebar: true,
        mini: false,
        temporary: false
    }),

    methods: {
        ToggleNavbar(){
            if(this.$vuetify.breakpoint.sm || this.$vuetify.breakpoint.xs){
                this.sidebar = !this.sidebar;
                this.mini = false;
                this.temporary = this.sidebar;
            }
            else{
                this.temporary = false;
                this.sidebar = true;
                this.mini = !this.mini;
            }
        }
    }
}
</script>

<style scoped>
.navbar-brand {
    font-size: 45px;
}
</style>