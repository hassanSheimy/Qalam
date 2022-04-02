<template>
    <v-card class="ma-3">
        <v-card-title class="padding fix-height">
            <p class="title">{{subject.subjectName}}</p>
            <v-spacer />
            <v-list-item-icon>
                <v-btn text icon>
                    <v-icon color="secondary" size="16" @click="EditClick">fas fa-edit</v-icon>
                </v-btn>
                <v-btn text icon>
                    <v-icon color="secondary" size="16" @click="RemoveClick">fas fa-trash</v-icon>
                </v-btn>
            </v-list-item-icon>
        </v-card-title>
        <v-card-actions class="padding">
            <v-list-item-icon>
                <v-icon color="secondary" size="16" class="ml-2">fas fa-chalkboard-teacher</v-icon>
                {{subject.yearName}}
            </v-list-item-icon>
            <v-list-item-icon>
                <v-icon color="secondary" size="16" class="ml-2">fas fa-book-open</v-icon>
                {{subject.semesterName}}
            </v-list-item-icon>
        </v-card-actions>
    </v-card>
</template>

<script>
export default {
    name: "TeachersubjectCard",

    props: {
        subject: {
            type: Object,
            required: true
        }
    },

    methods: {
        EditClick() {
            this.$emit("editClicked", this.subject.id)
        },
        RemoveClick() {
            this.$emit("deleteClicked", this.subject.id)
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