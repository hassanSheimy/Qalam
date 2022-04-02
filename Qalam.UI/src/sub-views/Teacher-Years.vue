<template>
    <v-container>
        <v-row no-gutters>
            <v-col cols="12">
                <v-btn color="primary" @click="AddYear">{{$locales.t('addTeacherYearBtn')}}</v-btn>
            </v-col>

            <v-col cols="12">
                <custom-card color="primary" :title="$locales.t('teacherAvailableYears')" customClass="white--text px-5 py-3 headline">
                    <teacher-years-list :courses="courses" @delete="DeleteClicked" />
                </custom-card>
            </v-col>

        </v-row>
        <!-- Dialogs -->
        <add-teacher-year-modal :dialog="addDialog" @close="AddModalClosed" @done="AddModalDone" />
        <delete-teacher-year-modal :dialog="deleteDialog" :course="selectedCourse" @close="DeleteModalClosed" @done="DeleteModalDone" />
    </v-container>
</template>

<script>
import CustomCard from '@/components/cards/custom-card.vue'
import TeacherYearsList from '@/components/lists/teacher-years-list.vue';
import AddTeacherYearModal from '@/components/modals/add-teacher-year-modal.vue';
import DeleteTeacherYearModal from '@/components/modals/delete-teacher-year-modal.vue';

export default {
    name: "TeacherYears",

    components: {
        CustomCard,
        TeacherYearsList,
        AddTeacherYearModal,
        DeleteTeacherYearModal
    },

    data: () => ({
        courses: [],
        addDialog: false,
        deleteDialog: false,
        selectedIndex: -1,
        selectedCourse: null
    }),

    mounted(){
        this.GetSubjects();
    },

    methods: {
        GetSubjects(){
            this.HttpRequest({
                url: "api/teacher/courses",
                auth: true
            }).then(this.GetSuccess)
            .catch(this.GetFailed);
        },
        GetSuccess(response){
            this.courses = response.data;
        },
        GetFailed(error){
            // error happend
        },
        DeleteClicked(index){
            this.selectedCourse = this.courses[index];
            console.log(this.selectedCourse, index);
            this.selectedIndex = index;
            this.deleteDialog = true
        },
        AddYear(){
            this.$eventBus.$emit('init-add-year-modal');
            this.addDialog = true;
        },
        AddModalClosed(){
            this.addDialog = false
        },
        AddModalDone(course){
            this.courses.push(course);
            this.addDialog = false
        },
        DeleteModalClosed(){
            this.deleteDialog = false
        },
        DeleteModalDone(){
            this.courses.splice(this.selectedIndex, 1);
            this.deleteDialog = false
        },
        PageChanged(page){

        }
    }
}
</script>

<style scoped>
</style>