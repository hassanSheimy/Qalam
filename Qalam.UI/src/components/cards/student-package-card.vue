<template>
    <custom-card class="v-card-profile text-center">
        <v-avatar slot="offset" class="mx-auto d-block v-card-offset" size="200">
            <img :src="studentPackage.image" >
        </v-avatar>

        <v-card-title class="border-top">
            {{studentPackage.name}}
        </v-card-title>

        <v-card-text>
            <v-chip v-for="(item, index) in studentPackage.items" :key="'p-' + index" class="ma-1" color="primary" outlined>
                {{`${item.name}: ${item.count}`}}
            </v-chip>
        </v-card-text>
    </custom-card>
</template>

<script>
import CustomCard from '@/components/cards/custom-card.vue'

export default {
    name: "StudentPackageCard",

    components: {
        CustomCard
    },

    props: {
        studentPackage: {
            type: Object,
            required: true
        }
    },

    data: () => ({
        
    }),
}
</script>

<style scoped>
.subtitle-1{
    margin-bottom: 0
}
.v-card-offset {
    top: unset !important;
    margin-bottom: unset !important;
    margin-top: -50px !important;
    box-shadow: 0 10px 30px -12px rgba(0, 0, 0, 0.42), 0 4px 25px 0px rgba(0, 0, 0, 0.12), 0 8px 10px -5px rgba(0, 0, 0, 0.2) !important;
}
</style>