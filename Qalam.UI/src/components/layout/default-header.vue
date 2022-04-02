<template>
    <div>
        <v-navigation-drawer v-model="sidemenu" app :right="$vuetify.rtl" temporary color="header">
            <side-bar-list />
        </v-navigation-drawer>

        <v-app-bar hide-on-scroll color="header" height="77" class="nav-bar" fixed>
            <v-toolbar-title class="primary--text app-arabic-font navbar-brand">
                <router-link to="/" class="remove-underline">
                        {{$locales.t('appName')}}
                        <span class="subtitle-2">{{$locales.t('beta')}}</span>
                </router-link>
            </v-toolbar-title>

            <v-app-bar-nav-icon class="hidden-md-and-up" @click="sidemenu = !sidemenu" :class="$vuetify.rtl ? 'mr-auto' : 'ml-auto'" />
            
            <div class="container fix-container hidden-sm-and-down">
                <header-tabs />
            </div>

            <exam-selector :dialog="dialog" @close="ModalClosed" @done="ModalDone" />
        </v-app-bar>
    </div>
</template>

<script>
import ExamSelector from '@/components/modals/exam-selector.vue'
import SideBarList from './side-bar-list.vue'
import HeaderTabs from './header-tabs.vue'

export default {
    name: "DefaultHeader",

    components: {
        ExamSelector,
        SideBarList,
        HeaderTabs
    },
    
    data: () => ({
        sidemenu: false,
        dialog: false
    }),

    created() {
        this.$eventBus.$on('show-exam-selector', () => this.dialog = true);
    },

    methods: {
        ModalClosed() {
            this.dialog = false
        },
        ModalDone() {
            this.dialog = false
        },
        GetText(role){
            var lang = this.$locales.current;
            return role === 'teacher' || role === 'student' ? this.$locales.t('personalPage') 
            : this.$locales.t('adminPage');
        }
    },
}
</script>

<style>
.navbar-brand {
    font-size: 45px;
}

.fix-container{
    padding-right: 0px;
}
</style>