<template>
    <v-container>
        <v-row no-gutters>
            <v-col cols="12">
                <custom-card class="v-card-profile" color="primary" :title="$locales.t('nextLessons')" customClass="white--text px-5 py-3">
                    <pagination-table server-update :map-data="MapData" :headers="headers" :api="api"/>
                </custom-card>
            </v-col>

            <v-col cols="12">
                <custom-card class="v-card-profile" color="primary" :title="$locales.t('watchedLessons')" customClass="white--text px-5 py-3">
                    <empty-state v-if="watchedLesson.length == 0" :message="$locales.t('noWatched')" />

                    <template v-else>    
                        <card-with-icons sm="6" cols="12" detailsBtn 
                        v-for="(lesson, index) in watchedLesson"
                        :key="'lesson-' + index" :btnText="$locales.t('watchBtn')" 
                        :data="lesson" :title="lesson.name" 
                        :btnPath="`/lesson/watch/${lesson.id}`" :icons="icons"/>
                    </template>

                    <pagination-component v-if="watchedLesson.length != 0" :length="pageCount" @changePage="changedPage"/>
                </custom-card>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
import PaginationTable from '@/components/shared/pagination-table.vue'
import CustomCard from '@/components/cards/custom-card.vue'
import CardWithIcons from '@/components/cards/card-with-icons.vue'
import EmptyState from '@/components/shared/empty-state.vue'
import PaginationComponent from '@/components/shared/pagination-component.vue'
import moment from 'moment'

export default {
    name: "StudentLessons",

    components: {
        PaginationTable,
        CustomCard,
        CardWithIcons,
        EmptyState,
        PaginationComponent
    },

    data: () => ({
        icons: [ 
            {icon: 'fas fa-book', value: 'subjectName'}, 
            {icon: 'fas fa-book-open', value: 'educationYear'},
            {icon: 'fas fa-chalkboard-teacher', value: 'teacherName'}
        ],
        watchedLesson:[],
        api: {
            url: "api/student/comming-lessons",
            auth: true
        },
        pageSize: 10,
        pageNo: 0,
        pageCount: 0
    }),

    computed: {
        headers() {
            return [
                { text: this.$locales.t('comminglessonsTable.date'), value: 'startDate', sortable: false},
                { text: this.$locales.t('comminglessonsTable.educationYearName'), value: 'educationYearName', sortable: false },
                { text: this.$locales.t('comminglessonsTable.subjectName'), value: 'subjectName', sortable: false },
                { text: this.$locales.t('comminglessonsTable.teacherName'), value: 'teacherName', sortable: false }
            ]
        }
    },

    mounted() {
        this.GetWatchedLessons()
    },

    methods: {
        GetWatchedLessons(){
            this.HttpRequest({
                url: 'api/student/watched-lessons',
                auth: true,
                params: {
                    pageSize: this.pageSize,
                    pageNo: this.pageNo
                }
            }).then(this.GetSuccess)
            .catch(this.GetFailed);
        },
        GetSuccess(response){
            this.watchedLesson = response.data.pageData;
            this.pageCount = Math.ceil(Math.max(response.data.count / Math.max(this.pageSize, 1), 1));
        },
        GetFailed(error){
            // error happened
        },
        changedPage(page){
            this.pageNo = page
            this.GetWatchedLessons();
        },
        MapData(data){
            var now = moment().unix();
            data.forEach(val => {
                val.startDate = moment.utc(val.startDate).local().locale(this.$locales.current).format('LLLL');
            });
            return data;
        }
    },
}
</script>

<style scoped>
</style>