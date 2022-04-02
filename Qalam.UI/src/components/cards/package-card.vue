<template>
    <v-card min-height="631" class="justify-center text-center ma-3 flexcard">
        <v-card-subtitle :class="{'headline': $vuetify.breakpoint.smAndDown, 'display-2': $vuetify.breakpoint.mdAndUp}" 
            class="display-1 my-3" :style="{color: packageObj.color}">
            {{packageObj.name}}
        </v-card-subtitle>
        <v-avatar class="mx-auto d-block v-card-offset" size="200">
            <img :src="packageObj.imagePath" class="elevation-10" />
        </v-avatar>
        <v-card-text>
            <v-list>
                <v-list-item-group>
                    <v-list-item v-for="(item, i) in packageObj.items" :key="'package-item-' + i" disabled :color="packageObj.color">
                        <v-list-item-content>
                            <v-list-item-title>
                                {{item.name}}
                                <!--span class="pricetag">{{item.priceInMoney}}</span-->
                            </v-list-item-title>
                        </v-list-item-content>
                    </v-list-item>
                </v-list-item-group>
            </v-list>
        </v-card-text>
        <v-card-actions class="justify-center">
            <v-btn :color="packageObj.color" large>
                {{$locales.t('bookBtn')}}
            </v-btn>
        </v-card-actions>
        <p v-if="packageObj.hasOffer" class="ribbon ribbon-index" :class="{right: $vuetify.rtl}"><span>{{$locales.t('specialOffer')}}</span></p>
    </v-card>
</template>

<script>

export default {
    name: "PackageCard",

    components: {
    },

    props: {
        packageObj: {
            type: Object,
            required: true
        }
    },

    data: () => ({
        checkbox1: false
    }),
}
</script>

<style scoped>
.flexcard {
    display: flex;
    flex-direction: column;
}
.ribbon-index{
    z-index:0;
}
</style>