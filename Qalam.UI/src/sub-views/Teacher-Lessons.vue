<template>
    <div class="teacher-lessons">
        <empty-state v-if="lessons.length == 0" />

        <v-container v-else>
            <v-row no-gutters >
                <card-with-icons md="6" cols="12" detailsBtn
                :btnText="$locales.t('detailsBtn')" 
                v-for="(lesson, index) in lessons" 
                :key="'lesson-' + index" :data="lesson" 
                :icons="icons" :title="lesson.name"
                :btnPath="`/profile/teacher/lesson/${lesson.id}`" />
            </v-row>
        </v-container>

        <pagination-component v-if="lessons.length != 0" :length="pageCount" @changePage="PageChanged"/>
    </div>
</template>

<script>
import EmptyState from '@/components/shared/empty-state.vue'
import CardWithIcons from '@/components/cards/card-with-icons.vue'
import PaginationComponent from '@/components/shared/pagination-component.vue'
import moment from 'moment'

export default {
    name: "TeacheLessons",

    components: {
        EmptyState,
        CardWithIcons,
        PaginationComponent
    },

    data: () => ({
        lessons: [],
        icons: [
            {icon: 'fas fa-book-open', value: 'educationYearName'},
            {icon: 'fas fa-calendar-day', value: 'showDate'}, 
            {icon: 'far fa-chart-bar', value: 'views'}, 
            {icon: 'fas fa-star', value: 'rating'}
        ],
        pageNo: 0,
        pageSize: 10,
        pageCount: 1,
    }),

    mounted(){
        this.GetExplanations();
    },

    methods: {
        GetExplanations(){
            this.HttpRequest({
                url: "api/teacher/explanations",
                params: {
                    educationYearId: null,
                    pageNo: this.pageNo,
                    pageSize: this.pageSize
                },
                auth: true
            }).then(this.GetSuccess)
            .catch(this.GetFailed);
        },
        GetSuccess(response){
            response.data.pageData.forEach(val => {
                val.views = `${this.$locales.t('views')}: ${val.views}`;
                val.rating = `${this.$locales.t('rating')}: ${val.rating}`;
                val.showDate = moment.utc(val.showDate).local().locale(this.$locales.current).format('LLLL');
            });
            this.lessons = response.data.pageData;
            this.pageCount = Math.ceil(Math.max(response.data.count / Math.max(this.pageSize, 1), 1));
        },
        GetFailed(error){
            // error happend
        },
        PageChanged(page){
            this.pageNo = page;
            this.GetExplanations();
        },
    }
}
</script>

<style scoped>

</style>