<template>
    <v-container>
        <v-row no-gutters>
            <v-col v-for="(studentPackage, index) in packages" :key="'package-' + index" cols="12" md="4" sm="6" class="pa-5">
                <student-package-card :studentPackage="studentPackage" />
            </v-col>

            <empty-state v-if="packages.length == 0" :message="$locales.t('noData')">
                <h4 v-if="false" class="ma-2">{{$locales.t('youCanBook')}}</h4>
                <v-btn v-if="false" class="ma-2" color="primary" outlined to="/packages">{{$locales.t('book')}}</v-btn>
            </empty-state>
        </v-row>
    </v-container>
</template>

<script>
import StudentPackageCard from '@/components/cards/student-package-card.vue'
import EmptyState from '@/components/shared/empty-state.vue'
import LivePackage from '@/assets/images/livePackage_.png'

export default {
    name: "StudentPackages",

    components: {
        StudentPackageCard,
        EmptyState
    },

    data: () => ({
        packages: [
            {
                image: LivePackage,
                name: "باقة قلم مباشر",
                items: [
                    {name: 'الامتحانات', count: '5'},
                    {name: 'مباشر', count: '7'}
                ]
            },
            {
                image: LivePackage,
                name: "باقة قلم مباشر",
                items: [
                    {name: 'الامتحانات', count: '5'},
                    {name: 'مباشر', count: '7'}
                ]
            }
        ]
    }),
}
</script>

<style scoped>
</style>