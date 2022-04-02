<template>
    <v-list three-line>
        <empty-state v-if="teachers.length == 0" />
        <div v-else class="min-height">
            <template v-for="(teacher, index) in teachers">
                <v-list-item :key="'teacher-' + index" :to="`/teacher/${teacher.id}`">
                    <v-list-item-avatar>
                        <v-img :src="teacher.imagePath || defaultImag"></v-img>
                    </v-list-item-avatar>

                    <v-list-item-content>
                        <v-list-item-title>
                            <v-badge v-if="!teacher.isConfirmed" overlap color="primary" content="لم يكمل التسجيل">
                                <span class="subtitle-1">{{teacher.fullName}}</span>
                            </v-badge>
                            <span v-else class="subtitle-1">{{teacher.fullName}}</span>
                        </v-list-item-title>
                        <v-list-item-subtitle>
                            <span class="subtitle-2">
                                {{teacher.subjectName}}
                            </span>
                        </v-list-item-subtitle>
                    </v-list-item-content>
                    <v-spacer />
                    <v-list-item-action>
                        <v-col>
                            <v-btn icon>
                                <v-icon size="20" color="secondary">fas fa-info-circle</v-icon>
                            </v-btn>
                        </v-col>
                    </v-list-item-action>
                </v-list-item>
                <v-divider :key="'divider-' + index" :inset="true" />
            </template>
        </div>
        <pagination-component v-if="teachers.length > 0" :length="pageCount" @changePage="ChangedPage" />
    </v-list>
</template>

<script>
import EmptyState from '@/components/shared/empty-state.vue'
import PaginationComponent from '@/components/shared/pagination-component.vue'
import UserDefaultImage from '@/assets/images/user.jpg'

export default {
    name: "TeachersList",

    components: {
        EmptyState,
        PaginationComponent
    },

    data: () => ({
        teachers: [],
        pageCount: 0,
        pageNo: 0,
        pageSize: 10,
        defaultImag: UserDefaultImage,
        filter: {}
    }),

    created() {
        this.$eventBus.$on("apply-filter", this.ApplyFilter);
        this.GetTeachers();
    },

    methods: {
        GetTeachers(){
            this.HttpRequest({
                url: 'api/teacher',
                params: {
                    countryId: this.filter.country,
                    educationTypeId: this.filter.educationType,
                    educationYearId: this.filter.educationYear,
                    semesterId: this.filter.semester,
                    subjectId: this.filter.subject,
                    pageNo: this.pageNo,
                    pageSize: this.pageSize
                },
                auth: true
            }).then(this.GetSuccess)
            .catch(this.GetFailed);
        },
        GetSuccess(response){
            this.teachers = response.data.pageData;
            this.pageCount = Math.ceil(Math.max(response.data.count / Math.max(this.pageSize, 1), 1));
        },
        GetFailed(error){
            // error happened
        },
        ApplyFilter(filter){
            this.filter = filter;
            this.GetTeachers();
        },
        ChangedPage(page){
            this.pageNo = page;
            this.GetTeachers();
        }
    }
}
</script>

<style scoped>
.min-height{
    min-height: 40vh;
}
</style>