<template>
    <v-container>
        <v-row no-gutters >
            <v-col cols="12">
                <v-btn color="primary" @click="addDialog = true">إضافة امتحان جديد</v-btn>
            </v-col>

            <v-col v-if="exams.length == 0" cols="12">
                <empty-state message="لم تقم بإضافة أي امتحان حتى الأن" />
            </v-col>

            <v-col v-else cols="12" class="exams-height">
                <teacher-exam-card v-for="(exam, index) in exams" :key="'exam-' + index" :exam="exam" @editClicked="EditClicked" @deleteClicked="DeleteClicked" />
            </v-col>
            
            <v-col v-if="exams.length != 0" cols="12">
                <pagination-component :length="pageCount" @changePage="PageChanged"/>
            </v-col>

            <!-- Dialogs -->
            <add-teacher-exam-modal :dialog="addDialog" @done="AddModalDone" @close="AddModalClosed" />
            <edit-teacher-exam-modal :dialog="editDialog" :exam="selectedExam" @done="EditModalDone" @close="EditModalClosed" />
            <delete-teacher-exam-modal :dialog="deleteDialog" :exam="selectedExam" @done="DeleteModalDone" @close="DeleteModalClosed" />
        </v-row>
    </v-container>
</template>

<script>
import EmptyState from "@/components/shared/empty-state.vue"
import TeacherExamCard from '@/components/cards/teacher-exam-card.vue'
import paginationComponent from "@/components/shared/pagination-component.vue"
import AddTeacherExamModal from '@/components/modals/add-teacher-exam-modal.vue'
import EditTeacherExamModal from '@/components/modals/edit-teacher-exam-modal.vue'
import DeleteTeacherExamModal from '@/components/modals/delete-teacher-exam-modal.vue'

export default {
    name: "TeacherExams",
    
    data: () => ({
        exams: [],
        addDialog: false,
        editDialog: false,
        deleteDialog: false,
        selectedExam: {
        },
        pageCount: 0,
        pageSize: 10
    }),

    mounted(){
        this.GetExams();
    },

    methods: {
        GetExams(){
            this.HttpRequest({
                url: "api/exam/teacher-exams",
                params: {
                    subjectId: null,
                    pageNo: this.pageNo,
                    pageSize: this.pageSize
                },
                auth: true
            }).then(this.GetSuccess)
            .catch(this.GetFailed);
        },
        GetSuccess(response){
            this.exams = response.data.pageData;
            this.pageCount = Math.ceil(Math.max(response.data.count / Math.max(this.pageSize, 1), 1));
        },
        GetFailed(error){
            // error happend
        },
        EditClicked(id){
            this.selectedExam = this.exams.find(e => e.id == id);
            if(this.selectedExam){
                this.editDialog = true
            }
        },
        DeleteClicked(id){
            this.selectedExam = this.exams.find(e => e.id == id);
            if(this.selectedExam){
                this.deleteDialog = true
            }
        },
        AddModalClosed(){
            this.addDialog = false
        },
        AddModalDone(){
            this.addDialog = false
        },
        EditModalClosed(){
            this.editDialog = false
        },
        EditModalDone(){
            this.editDialog = false
        },
        DeleteModalClosed(){
            this.deleteDialog = false
        },
        DeleteModalDone(){
            this.deleteDialog = false
        },
        PageChanged() {

        }
    },
    components: {
        EmptyState,
        TeacherExamCard,
        paginationComponent,
        AddTeacherExamModal,
        EditTeacherExamModal,
        DeleteTeacherExamModal
    }
}
</script>

<style scoped>
.exams-height{
    min-height: 50vh;
}
</style>