<template>
    <v-container fluid>
        <v-row justify="center">
            <v-col md="5" cols="11" class="align-self-center" :class="$vuetify.rtl ? 'text-right' : 'text-left'">
                <h1 class="primary--text">{{$locales.t('withoutBorders')}}</h1>
                <p>
                    {{$locales.t('accessFromAnyWhere')}}
                </p>
            </v-col>
            <v-col md="4" cols="11">
                <img src="@/assets/images/Pi.png" style="max-width: 100%; max-height: 300%">
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
export default {
    name: "AccessAnywhere"
}
</script>