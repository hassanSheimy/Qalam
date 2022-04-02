<template>
    <div class="packages-page">
        <default-header />
        
        <v-container class="my-4 min-height">
            <v-row no-gutters justify="center">
                <empty-state v-if="packages.length == 0" :message="$locales.t('noData')" />
                
                <v-col v-for="(packageObj, index) in packages" :key="'package-card-' + index" cols="10" md="4" sm="6">
                    <package-card :packageObj="packageObj"/>
                </v-col>
            </v-row>
        </v-container>

        <default-footer />
    </div>
</template>

<script>
import DefaultHeader from '@/components/layout/default-header.vue'
import EmptyState from '@/components/shared/empty-state.vue'
import PackageCard from '@/components/cards/package-card.vue'
import DefaultFooter from '@/components/layout/default-footer.vue'
import LiveImage from '@/assets/images/livePackage_.png'

export default {
    name: "Packages",

    components: {
        DefaultHeader,
        EmptyState,
        PackageCard,
        DefaultFooter
    },

    data: () => ({
        packages: []
    }),

    mounted(){
        this.GetPackages();
    },

    methods: {
        GetPackages(){
            this.HttpRequest({
                url: "api/package/listing",
            }).then(this.GetSuccess)
            .catch(this.GetFailed);
        },
        GetSuccess(response){
            this.packages = response.data;
        },
        GetFailed(error){
            // error happend
        }
    }
}
</script>

<style scoped>
.min-height{
    min-height: 80vh;
}
</style>