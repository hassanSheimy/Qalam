<template>
    <v-container>
        <v-row no-gutters>
            <v-col cols="12">
                <v-btn color="primary" @click="addDialog = true">{{$locales.t('addNewQuestionsBtn')}}</v-btn>
            </v-col>
            <v-col cols="12">
                <custom-card color="primary" :title="$locales.t('availableQuestions')" customClass="white--text px-5 py-3 headline">
                    <questions-list />
                </custom-card>
            </v-col>
        </v-row>
        <add-questions-modal :dialog="addDialog" @done="AddModalDone" @close="AddModalClosed" />
    </v-container>
</template>

<script>
import CustomCard from '@/components/cards/custom-card.vue'
import QuestionsList from '@/components/lists/questions-list.vue'
import AddQuestionsModal from '@/components/modals/add-questions-modal.vue'

export default {
    name: "AdminQuestions",

    components: {
        CustomCard,
        QuestionsList,
        AddQuestionsModal
    },

    data: () => ({
        addDialog: false
    }),

    methods: {
        AddModalDone(){
            this.addDialog = false;
        },
        AddModalClosed(){
            this.addDialog = false;
        }
    },
}
</script>

<style scoped>

</style>