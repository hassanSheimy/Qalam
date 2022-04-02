<template>
    <v-footer class="justify-center border-top" v-bind="$attrs">
        <p class="title text-center">
            {{$locales.t('rights')}}<span class="primary--text">{{$locales.t('appName')}} </span>
            {{fullYear}}  &copy;
        </p>
    </v-footer>
</template>

<script>
export default {
    name: "SecondaryFooter",

    data: () => ({
        fullYear: new Date().getFullYear(),
    })
}
</script>

<style scoped>
.secondary-footer{
    bottom: 0;
}
</style>