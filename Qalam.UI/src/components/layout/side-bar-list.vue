<template>
    <v-list color="header">
        <v-list-item class="py-1 secondary--hover justify-start" to="/" exact>
            <v-list-item-icon>
                <v-icon :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">fas fa-home</v-icon>
            </v-list-item-icon>
            <v-list-item-content>
                <v-list-item-title class="subtitle-1" 
                :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">
                    {{$locales.t('home')}}
                </v-list-item-title>
            </v-list-item-content>
        </v-list-item>

        <v-list-item class="py-1 secondary--hover justify-start" to="/live" exact>
            <v-list-item-icon>
                <v-icon :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">fas fa-video</v-icon>
            </v-list-item-icon>
            <v-list-item-content>
                <v-list-item-title class="subtitle-1" 
                :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">            
                    {{$locales.t('appName')}} {{$locales.t('live')}}
                </v-list-item-title>
            </v-list-item-content>
        </v-list-item>

        <v-list-item class="secondary--hover justify-start" to="/teachers" exact>
            <v-list-item-icon>
                <v-icon :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">fas fa-chalkboard-teacher</v-icon>
            </v-list-item-icon>
            <v-list-item-content>
                <v-list-item-title class="subtitle-1" 
                :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">
                    {{$locales.t('teachers')}}
                </v-list-item-title>
            </v-list-item-content>
        </v-list-item>

        <v-list-item disabled class="secondary--hover justify-start">
            <v-list-item-icon>
                <v-badge overlap color="primary" :content="$locales.t('soonBadge')"> 
                    <v-icon :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">fas fa-file-alt</v-icon>
                </v-badge>
            </v-list-item-icon>
            <v-list-item-content>
                <v-list-item-title class="subtitle-1" 
                :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">    
                    {{$locales.t('exams')}}
                </v-list-item-title>
            </v-list-item-content>
        </v-list-item>

        <v-list-item disabled class="py-n2 secondary--hover justify-start" to="" exact>
            <v-list-item-icon>
                <v-badge overlap color="primary" :content="$locales.t('soonBadge')"> 
                    <v-icon :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">fas fa-trophy</v-icon>
                </v-badge>
            </v-list-item-icon>
            <v-list-item-content>
                <v-list-item-title class="subtitle-1" 
                :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">
                    {{$locales.t('contests')}}
                </v-list-item-title>
            </v-list-item-content>
        </v-list-item>

        <v-list-item disabled class="py-n secondary--hover justify-start" to="" exact>
            <v-list-item-icon>
                <v-badge overlap color="primary" :content="$locales.t('soonBadge')"> 
                    <v-icon :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">fas fa-box-open</v-icon>
                </v-badge>
            </v-list-item-icon>
            <v-list-item-content>
                <v-list-item-title class="subtitle-1" 
                :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">
                    {{$locales.t('packages')}}
                </v-list-item-title>
            </v-list-item-content>
        </v-list-item>

        <v-list-item v-if="!login" class="py-n2 secondary--hover justify-start" to="/signin" exact>
            <v-list-item-icon>
                <v-icon :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">fas fa-sign-in-alt</v-icon>
            </v-list-item-icon>
            <v-list-item-content>
                <v-list-item-title class="subtitle-1" 
                :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">
                    {{$locales.t('signin')}}
                </v-list-item-title>
            </v-list-item-content>
        </v-list-item>

        <v-list-item v-if="!login" class="py-1 secondary--hover justify-start" to="/signup" exact>
            <v-list-item-icon>
                <v-icon :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">fas fa-user-plus</v-icon>
            </v-list-item-icon>
            <v-list-item-content>
                <v-list-item-title class="subtitle-1" 
                :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">
                    {{$locales.t('signup')}}
                </v-list-item-title>
            </v-list-item-content>
        </v-list-item>

        <v-list-group v-if="login && user.roles.length > 1" v-model="active" prepend-icon="fas fa-user" 
        :color="$vuetify.theme.isDark ? 'white' : 'black'">
            <v-list-item slot="activator">
                <v-list-item-content>
                    <v-list-item-title v-text="user.fullName" />
                </v-list-item-content>
            </v-list-item>
            <v-list-item v-for="role in user.roles" :key="'user-role-' + role">
                <router-link :to="'/profile/' + role" tag="a" class="link remove-underline"
                :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">
                    <v-list-item-action v-text="GetText(role)" />
                </router-link>
            </v-list-item>
        </v-list-group>

        <v-list-item v-if="login && user.roles.length == 1" :to="'/profile/' + user.roles[0]" class="py-1 secondary--hover justify-start">
            <v-list-item-icon>
                <v-icon :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">fas fa-user</v-icon>
            </v-list-item-icon>
            <v-list-item-content>
                <v-list-item-title class="subtitle-1" 
                :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">
                    {{user.fullName}}
                </v-list-item-title>
            </v-list-item-content>
        </v-list-item>    

        <v-list-item v-if="login" class="py-1 secondary--hover justify-start" to="/logout">
            <v-list-item-icon>
                <v-icon :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">fas fa-door-open</v-icon>
            </v-list-item-icon>
            <v-list-item-content>
                <v-list-item-title class="subtitle-1" 
                :class="$vuetify.theme.isDark ? 'white--text' : 'black--text'">
                    {{$locales.t('logout')}}
                </v-list-item-title>
            </v-list-item-content>
        </v-list-item>
    </v-list>
</template>

<script>
export default {
    name: "SideBarList",

    data: () => ({
        login: false,
        active: false
    }),

    created() {
        this.login = this.IsThereUser();
    },

    methods: {
        GetText(role){
            return role === 'teacher' || role === 'student' ? this.$locales.t('personalPage') : this.$locales.t('adminPage');
        },
        ShowExamSelector(){
            this.$eventBus.$emit('show-exam-selector');
        }
    }
}
</script>

<style scoped>
.v-list-item{
    margin: 12px 0;
}
</style>