<template>
    <v-card class="exam-card ma-3">
        <v-col cols="12">
            <v-card-title class="padding fix-height">
                <p class="title">{{exam.name}}</p>
                <v-list-item-icon class="exam-card-icon mb-7">
                    <v-btn text icon @click="EditClick">
                        <v-icon color="secondary" size="16">fas fa-edit</v-icon>
                    </v-btn>
                    <v-btn text icon @click="RemoveClick">
                        <v-icon color="secondary" size="16">fas fa-trash</v-icon>
                    </v-btn>
                    <v-btn text icon class="hidden-sm-and-up" :to="`/profile/teacher/exam/${exam.id}`">
                        <v-icon color="secondary" size="16">fas fa-info-circle</v-icon>
                    </v-btn>
                </v-list-item-icon>
                <v-spacer />
                <v-btn color="primary" class="hidden-xs-only" :to="`/profile/teacher/exam/${exam.id}`">{{$locales.t('detailsBtn')}}</v-btn>
            </v-card-title>
        </v-col>
        <v-card-actions class="padding">
            <v-list-item-icon>
                <v-icon color="secondary" size="16" class="ml-2">fas fa-book-open</v-icon>
                {{exam.subjectName}}
            </v-list-item-icon>
            <v-list-item-icon>
                <v-icon color="secondary" size="16" class="ml-2">fa-calendar-day</v-icon>
                {{new Date(exam.date).toDateString()}}
            </v-list-item-icon>
        </v-card-actions>
    </v-card>
</template>

<script>
export default {
    name: "TeacherExamCard",

    props: {
        exam: {
            type: Object,
            required: true
        }
    },

    methods: {
        EditClick() {
            this.$emit("editClicked", this.exam.id)
        },
        RemoveClick() {
            this.$emit("deleteClicked", this.exam.id)
        }
    }
}
</script>

<style scoped>
.padding{
    padding: 0 20px;
}
.fix-height{
    height: 40px;
}
</style>