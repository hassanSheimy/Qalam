<template>
    <v-list>
        <v-row no-gutters>
            <v-text-field @click="searchDialog = true" append-icon="fas fa-search" :label="$locales.t('search')" />
        </v-row>
        <empty-state v-if="questions.length == 0" />
        <template v-for="(question, index) in questions">
            <v-list-item :key="'question-' + index">
                <v-list-item-content>
                    <v-list-item-title>
                        <span class="subtitle-1">{{question.text}}</span>
                    </v-list-item-title>
                    <v-list-item-subtitle>
                        <span class="subtitle-2">
                            {{question.answer}}
                        </span>
                    </v-list-item-subtitle>
                </v-list-item-content>
                <v-spacer />
                <v-list-item-action>
                    <v-col>
                        <v-btn icon>
                            <v-icon size="20" color="secondary">fas fa-pen</v-icon>
                        </v-btn>
                        <v-btn icon>
                            <v-icon size="20" color="secondary">fas fa-trash</v-icon>
                        </v-btn>
                    </v-col>
                </v-list-item-action>
            </v-list-item>
            <v-divider :key="'divider-' + index" :inset="true" />
        </template>
        <pagination-component v-if="questions.length > 0" :length="10" @changePage="ChangedPage" />
        <question-search-modal :dialog="searchDialog" @done="SearchModalDone" @close="SearchModalClosed" />
    </v-list>
</template>

<script>
import EmptyState from '@/components/shared/empty-state.vue'
import PaginationComponent from '@/components/shared/pagination-component.vue'
import QuestionSearchModal from '@/components/modals/question-search-modal.vue'

export default {
    name: "TeachersList",

    components: {
        EmptyState,
        PaginationComponent,
        QuestionSearchModal
    },

    data: () => ({
        questions: [
            {
                text: "Abdalrahman Ahmed",
                answer: "Hello"
            },
            {
                text: "Abdalrahman Ahmed",
                answer: "Hello"
            },
            {
                text: "Abdalrahman Ahmed",
                answer: "Hello"
            },
            {
                text: "Abdalrahman Ahmed",
                answer: "Hello"
            },
            {
                text: "Abdalrahman Ahmed",
                answer: "Hello"
            },
            {
                text: "Abdalrahman Ahmed",
                answer: "Hello"
            },
            {
                text: "Abdalrahman Ahmed",
                answer: "Hello"
            },
            {
                text: "Abdalrahman Ahmed",
                answer: "Hello"
            }
        ],
        searchDialog: false
    }),

    methods: {
        ChangedPage(page){

        },
        SearchModalDone(){
            this.searchDialog = false
        },
        SearchModalClosed(){
            this.searchDialog = false
        }
    }
}
</script>

<style scoped>

</style>