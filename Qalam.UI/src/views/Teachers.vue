<template>
    <div class="teachers-page">
        <default-header />

        <v-container class="min-height">
            <v-row no-gutters>        
                <filter-component @filter="ApplyFilter" />
            </v-row>
            <v-row v-if="teachers.length == 0" no-gutters>
                <empty-state :message="$locales.t('noData')" />
            </v-row>
            <v-row v-else no-gutters class="teacher-card text-center">
                <v-col v-for="(teacher, index) in teachers" :key="'teacher-' + index" lg="4" md="6" cols="12" class="my-10">
                    <account-card :userObject="teacher"/>
                </v-col>
            </v-row>
            <v-row no-gutters v-if="teachers.length != 0">
                <v-col align-self="center">
                    <pagination-component :length="pageCount" @changePage="PageChanged"/>
                </v-col>
            </v-row>
        </v-container>
        <default-footer />
    </div>
</template>

<script>
import DefaultHeader from '@/components/layout/default-header.vue'
import FilterComponent from '@/components/shared/filter-component.vue'
import EmptyState from '@/components/shared/empty-state.vue'
import AccountCard from '@/components/cards/account-card.vue'
import PaginationComponent from '@/components/shared/pagination-component.vue'
import DefaultFooter from '@/components/layout/default-footer.vue'

export default {
    name: "Teachers",

    components: {
        DefaultHeader,
        FilterComponent,
        EmptyState,
        AccountCard,
        PaginationComponent,
        DefaultFooter
    },

    data: () => ({
        teachers: [],
        pageNo: 0,
        pageSize: 10,
        pageCount: 10,
        filter: {
            countryId: null,
            educationTypeId: null,
            educationYearId: null,
            semesterId: null,
            subjectId: null
        }
    }),

    mounted() {
        this.GetTeachers();
    },

    methods: {
        GetTeachers(){
            this.HttpRequest({
                url: "api/teacher/listing",
                params: {
                    countryId: this.filter.countryId,
                    educationTypeId: this.filter.educationTypeId,
                    educationYearId: this.filter.educationYearId,
                    subjectId: this.filter.subjectId,
                    pageNo: this.pageNo,
                    pageSize: this.pageSize
                } 
            }).then(this.GetSuccess)
            .catch(this.GetFailed);
        },
        GetSuccess(response){
            this.teachers = response.data.pageData;
            this.pageCount = Math.ceil(Math.max(response.data.count / Math.max(this.pageSize, 1), 1));
        },
        GetFailed(error){
            // error happend
        },
        PageChanged(page){
            this.pageNo = page;
            this.GetTeachers();
        },
        ApplyFilter(filter){
            this.filter = filter;
            this.pageNo = 1;
            this.GetTeachers();
        }
    }
}
</script>

<style scoped>
.teacher-card{
    margin-top: 40px;
}
.min-height{
    min-height: 80vh;
}
</style>