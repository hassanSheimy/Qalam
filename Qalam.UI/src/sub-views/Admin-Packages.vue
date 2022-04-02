<template>
    <v-container>
        <v-row>
            <v-col cols="12">
                <v-btn color="primary" @click="addDialog = true">{{$locales.t('addNewPackageBtn')}}</v-btn>
            </v-col>
        </v-row>
        <v-row no-gutters>
            <empty-state v-if="packages.length == 0" message="لا توجد اي باقات حاليا" />
            
            <card-with-icons sm="6" cols="12" can-edit can-delete detailsBtn
                :btnText="$locales.t('detailsBtn')"
                v-for="(packageObj, index) in packages" 
                :key="'package-' + index" :data="packageObj" 
                :icons="icons" :title="packageObj.name"
                :btnPath="`/profile/admin/package/${index}`" 
                @editClicked="EditClicked"
                @deleteClicked="DeleteClicked" />
        </v-row>
    </v-container>
</template>

<script>
import EmptyState from '@/components/shared/empty-state.vue'
import CardWithIcons from '@/components/cards/card-with-icons.vue'

export default {
    name: "AdminPackages",

    components: {
        EmptyState,
        CardWithIcons
    },

    data: () => ({
        addDialog: false,
        packages: [{
            name: "باقة قلم مباشر",
            studentsCount: "500 طالب مشترك",
            numberOfItems: "4 امتحانات",
            numberOfOffers: "10 دروس مباشر",
            cost: "سعر الباقة 500$"
        },{
            name: "باقة قلم مباشر",
            studentsCount: "500 طالب مشترك",
            numberOfItems: "4 امتحانات",
            numberOfOffers: "10 دروس مباشر",
            cost: "سعر الباقة 500$"
        },{
            name: "باقة قلم مباشر",
            studentsCount: "500 طالب مشترك",
            numberOfItems: "4 امتحانات",
            numberOfOffers: "10 دروس مباشر",
            cost: "سعر الباقة 500$"
        }],
        icons: [
            {icon: 'fas fa-chart-bar', value: 'studentsCount'}, 
            {icon: 'fas fa-file-alt', value: 'numberOfItems'}, 
            {icon: 'fas fa-video', value: 'numberOfOffers'}
        ],
    }),

    methods: {
        EditClicked(){

        },
        DeleteClicked(){

        }
    }
}
</script>

<style scoped>

</style>