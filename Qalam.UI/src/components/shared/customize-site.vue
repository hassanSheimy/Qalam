<template>
    <div id="settings-wrapper">
        <v-card id="settings" class="py-2 px-4 setting" :class="{right: $vuetify.rtl}" dark link min-width="20">
            <v-icon large>
                fas fa-cogs
            </v-icon>
        </v-card>

        <v-menu v-model="menu" :close-on-content-click="false" activator="#settings" bottom content-class="v-settings"
            :right="$vuetify.rtl" :left="!$vuetify.rtl" offset-x 
            :origin="($vuetify.rtl ? 'top left' : 'top right')" transition="scale-transition">
            <v-card class="text-center mb-0" width="300">
                <v-card-text>
                    <strong class="mb-3 d-inline-block">{{$locales.t('siteSettings')}}</strong>
                    <v-row align="center" no-gutters>
                        <v-col cols="auto">{{$locales.t('darkMode')}}</v-col>
                        <v-spacer />
                        <v-col cols="auto">
                            <v-switch
                                v-model="$vuetify.theme.dark"
                                @change="ChangeMode"
                                class="ma-0 pa-0"
                                color="primary"
                                hide-details />
                        </v-col>
                    </v-row>

                    <v-row align="center" no-gutters class="mt-4">
                        <v-col cols="auto">{{$locales.t('language')}}</v-col>
                        <v-spacer />
                        <v-col cols="auto">
                            <v-item-group>
                                <v-item v-for="language in languages" :key="language.code" 
                                :value="language.name">
                                    <v-chip active-class="primary--text"
                                    :input-value="choosenLang == language.code" @click="ChangeLanguage(language)">
                                        {{language.name}}
                                    </v-chip>
                                </v-item>
                            </v-item-group>
                        </v-col>
                    </v-row>
                </v-card-text>
            </v-card>
        </v-menu>
    </div>
</template>

<script>
export default {
    name: 'CustomizeSite',

    data: () => ({
        languages: [{name: 'English', code: "en"}, {name: 'العربية', code: 'ar'}],
        menu: false,
        choosenLang: 'ar'
    }),

    created(){
        this.choosenLang = localStorage.lang || 'ar'
        this.$locales.setLocale(this.choosenLang);
        this.$vuetify.rtl = this.$locales.isRTL();
    },

    methods: {
        ChangeMode(darkMode){
            localStorage.darkMode = darkMode;
            // change theme meta 
            document.querySelector("head meta[name='theme-color']").content = 
            this.$vuetify.theme.themes[this.$vuetify.theme.isDark ? 'dark' : 'light'].systemBar
            
            this.$eventBus.$emit("dark-mode", darkMode);
        },
        ChangeLanguage(language){
            if(language.code == this.choosenLang){
                return;
            }
            this.choosenLang = language.code;
            this.menu = false;
            this.$locales.current = language.code;
            localStorage.lang = language.code;
            this.$router.go();
        }
    }
}
</script>

<style scoped>
.setting, .setting:active{
    position: fixed; 
    top: 25%;
    border-top-left-radius: 8px !important;
    border-bottom-left-radius: 8px !important;
    border-top-right-radius: 0px !important;
    border-bottom-right-radius: 0px !important;
    z-index: 201;
    right: 0;
    background-color: rgba(0, 0, 0, 0.5) !important;
}
.setting.right{
    border-top-right-radius: 8px !important;
    border-bottom-right-radius: 8px !important;
    border-top-left-radius: 0px !important;
    border-bottom-left-radius: 0px !important;
    right: initial;
    left: 0;
}
.v-settings .v-item-group > * {
    cursor: pointer;
}
.v-settings__item {
    border-width: 3px;
    border-style: solid;
    border-color: transparent !important;
}

</style>