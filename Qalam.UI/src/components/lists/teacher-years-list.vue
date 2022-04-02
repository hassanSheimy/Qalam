<template>
    <v-list>
        <empty-state v-if="courses.length == 0" />
        <div v-else class="min-height">
            <template v-for="(course, index) in courses">
                <v-list-item :key="'year-' + index">
                    <v-list-item-content>
                        <v-list-item-title>
                            <span class="subtitle-1">{{course.name}}</span>
                        </v-list-item-title>
                    </v-list-item-content>
                    <v-spacer />
                    <v-list-item-action>
                        <v-col>
                            <v-btn icon @click="DeleteClicked(index)">
                                <v-icon size="20" color="secondary">fas fa-trash</v-icon>
                            </v-btn>
                        </v-col>
                    </v-list-item-action>
                </v-list-item>
                <v-divider :key="'divider-' + index" inset />
            </template>
        </div>
    </v-list>
</template>

<script>
import EmptyState from '@/components/shared/empty-state.vue'
import PaginationComponent from '@/components/shared/pagination-component.vue'

export default {
    name: "TeacherYearsList",

    components: {
        EmptyState,
    },

    props: {
        courses: {
            type: Array,
            required: true
        }
    },

    methods: {
        DeleteClicked(index){
            this.$emit('delete', index)
        }
    }
}
</script>

<style scoped>
.min-height{
    min-height: 40vh;
}
</style>