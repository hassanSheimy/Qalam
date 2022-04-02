<template>
    <v-container>
        <v-row no-gutters>
            <v-col cols="12">
                <custom-card color="primary" :title="$locales.t('availableTeachers')" customClass="white--text px-5 py-3 headline">
                    <filter-component @filter="ApplyFilter" />
                    
                    <teachers-list />
                </custom-card>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
import CustomCard from '@/components/cards/custom-card.vue'
import FilterComponent from '@/components/shared/filter-component.vue'
import TeachersList from '@/components/lists/teachers-list.vue'

export default {
    name: "AdminTeachers",

    components: {
        CustomCard,
        FilterComponent,
        TeachersList
    },

    data: () => ({
        addDialog: false
    }),

    methods: {
        ApplyFilter(filter){
            this.$eventBus.$emit("apply-filter", filter);
        }
    }
}
</script>

<style scoped>

</style>