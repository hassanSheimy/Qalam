<template>
    <v-app class="app-arabic-font main-application accent" :class="{padding: havePadding}"
    :style="{ backgroundColor: $vuetify.theme.themes[$vuetify.theme.isDark ? 'dark' : 'light'].background}">
        <v-content>
            <router-view />
        </v-content>
        <v-btn v-scroll="onScroll" v-show="fab" fab fixed bottom left color="primary" @click="toTop">
            <v-icon>fas fa-arrow-up</v-icon>
        </v-btn>
        <loading-spainner />
        <customize-site />
        <confirm-modal ref="confirm" />
        <notifications group="Qalam" :position="`bottom ${$vuetify.rtl ? 'right' : 'left'}`" classes="notifications" />
    </v-app>
</template>

<script>
import TitleMixin from '@/mixins/title.js'
import LoadingSpainner from '@/components/shared/loading-spainner.vue'
import CustomizeSite from '@/components/shared/customize-site.vue'
import ConfirmModal from '@/components/modals/confirm-modal.vue'

export default {
    name: 'App',

    components: {
        LoadingSpainner,
        CustomizeSite,
        ConfirmModal
    },

    mixins: [TitleMixin],

    data: () => ({
        fab: false,
        havePadding: true,
    }),

    computed: {
        title() {
            return this.$locales.t('appName');
        }
    },

    created(){
        this.$locales.setLocale(localStorage.lang || 'ar');
        this.$vuetify.rtl = this.$locales.isRTL();
        this.$vuetify.theme.dark = localStorage.darkMode == 'true';
        // Change theme meta 
        document.querySelector("head meta[name='theme-color']").content = 
        this.$vuetify.theme.themes[this.$vuetify.theme.isDark ? 'dark' : 'light'].systemBar

        this.$qalamHub.$on('existing-user', this.UserAlreadyLogin)
        this.$eventBus.$on('toggle-app-padding', (state) => this.havePadding = state);
    },

    mounted(){
        this.$root.$confirm = this.$refs.confirm.open;
        this.InitApp();
    },
    
    methods: {
        InitApp(){
            // get coutries details for lookups
            this.HttpRequest({
                url: "api/countries/lookups"   
            }).then(this.GetCountriesSuccess)
            .catch(this.GetCountriesFailed)

            // get languages
            this.HttpRequest({
                url: "api/language"   
            }).then(this.GetLanguagesSuccess)
            .catch(this.GetLanguagesFailed)
        },
        onScroll (e) {
            if (typeof window === 'undefined') return
            const top = window.pageYOffset || e.target.scrollTop || 0
            this.fab = top > 300
        },
        toTop () {
            this.$vuetify.goTo(0)
        },
        GetCountriesSuccess(response){
            this.$store.commit("InitCountries", response.data);
        },
        GetCountriesFailed(error){
            // error happend
        },
        GetLanguagesSuccess(response){
            this.$store.commit("InitLanguages", response.data);
        },
        GetLanguagesFailed(error){
            // error happend
        },
        UserAlreadyLogin(){
            this.userExists = true;
        }
    },
};
</script>

<style>
.main-application.padding{
    padding-top: 77px;
}
.notifications{
    padding: 10px;
    margin: 0 5px 5px;

    font-size: 12px;

    color: #ffffff;
    background: #44A4FC;
    border-left: 5px solid #187FE7;
}

.notifications .notification-title {
    font-weight: 600;
    text-align: start;
}

.notifications .notification-content {
    text-align: start;
}
.notifications.primary{
    background: #79a05f;
}
.notifications.warning{
    background: #ffb648;
}
.notifications.error{
    background: #b71c1c;
}

</style>