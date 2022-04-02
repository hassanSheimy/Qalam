<template>
    <v-tabs background-color="header" show-arrows>
        <v-tabs-slider color="teal lighten-3"></v-tabs-slider>
        <v-tab to="/">{{$locales.t('home')}}</v-tab>
        <v-tab to="/live">{{$locales.t('appName')}} {{$locales.t('live')}}</v-tab>
        <v-tab to="/teachers">{{$locales.t('teachers')}}</v-tab>
        
        <v-badge overlap color="primary" class="badge-to-tab" :class="{right: $vuetify.rtl}" :content="$locales.t('soonBadge')">
            <v-tab disabled @click="dialog = true">{{$locales.t('exams')}}</v-tab>
        </v-badge>
        <v-badge overlap color="primary" class="badge-to-tab" :class="{right: $vuetify.rtl}" :content="$locales.t('soonBadge')">
            <v-tab disabled to="/contests">{{$locales.t('contests')}}</v-tab>
        </v-badge>
        <v-badge overlap color="primary" class="badge-to-tab" :class="{right: $vuetify.rtl}" :content="$locales.t('soonBadge')">
            <v-tab disabled to="/packages">{{$locales.t('packages')}}</v-tab>
        </v-badge>
        
        <v-spacer />
        
        <v-tab v-if="!login" to="/signin">{{$locales.t('signin')}}</v-tab>
        <v-tab v-if="!login" to="/signup">{{$locales.t('signup')}}</v-tab>

        <v-menu v-if="login && user.roles.length > 1" transition="slide-y-transition" nudge-bottom="58">
            <template v-slot:activator="{ on }">
                <v-btn class="mr-auto btn-to-tab v-tab" text v-on="on">{{ user.fullName }}</v-btn>
            </template>
            <v-list>
                <v-btn v-for="role in user.roles" :key="role" large :to="'/profile/' + role" text class="d-block justify-start v-tab">{{ GetText(role) }}</v-btn>
            </v-list>
        </v-menu>
        <v-btn v-if="login && user.roles.length == 1" :to="'/profile/' + user.roles[0]" text class="mr-auto btn-to-tab v-tab">{{ user.fullName }}</v-btn>
        <v-btn v-if="login" class="mr-auto btn-to-tab v-tab" text @click="login = false" to="/logout">{{$locales.t('logout')}}</v-btn>
    </v-tabs>
</template>

<script>
export default {
    name: "HeaderTabs",

    data: () => ({
        login: false
    }),

    created() {
        this.login = this.IsThereUser();
    },

    methods: {
        GetText(role){
            return role === 'teacher' || role === 'student' ? this.$locales.t('personalPage') : this.$locales.t('adminPage');
        }
    }
}
</script>

<style>
.navbar-brand {
    font-size: 45px;
}

.fix-container{
    padding-right: 0px;
}

.btn-to-tab{
    top: 6px;
}

.badge-to-tab{
    top: 10px;
}
.badge-to-tab span.v-badge__wrapper{
    top: -2px !important;
    left: -14px !important;
}
.right.badge-to-tab span.v-badge__wrapper{
    left: 14px !important;
}

.btn-bg-transparent::before{
    background-color: transparent;
}
</style>