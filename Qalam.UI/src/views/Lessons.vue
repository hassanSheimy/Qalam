<template>
    <div class="live-page">
        <default-header />
        <v-container class="my-4 align-center justify-center min-height">
            <v-row no-gutters justify="center">
                <filter-component @filter="ApplyFilter" />
            </v-row>
            <v-row no-gutters v-if="lessons.length == 0">
                <empty-state :message="$locales.t('noData')" />
            </v-row>
            <v-row v-else no-gutters>
                <v-col v-for="(packageObj, index) in lessons" :key="'lesson-card-' + index" cols="12">
                    <live-lesson-card :lesson="packageObj"/>
                </v-col>
            </v-row>
            <v-row no-gutters v-if="lessons.length != 0">
                <v-col align-self="center">
                    <pagination-component :length="pageCount" @changePage="PageChanged"/>
                </v-col>
            </v-row>
        </v-container>
        <default-footer />
    </div>
</template>

<script>
import TitleMixin from '@/mixins/title.js'
import DefaultHeader from '@/components/layout/default-header.vue'
import FilterComponent from '@/components/shared/filter-component.vue'
import EmptyState from '@/components/shared/empty-state.vue'
import LiveLessonCard from '@/components/cards/live-lesson-card.vue'
import PaginationComponent from '@/components/shared/pagination-component.vue'
import DefaultFooter from '@/components/layout/default-footer.vue'

export default {
    name: "Live",

    components: {
        DefaultHeader,
        FilterComponent,
        EmptyState,
        LiveLessonCard,
        PaginationComponent,
        DefaultFooter
    },

    mixins: [TitleMixin],

    data: () => ({
        lessons: [],
        pageNo: 1,
        pageSize: 10,
        pageCount: 1,
        filter: {
            countryId: null,
            educationTypeId: null,
            educationYearId: null,
            semesterId: null,
            subjectId: null
        }
    }),

    computed: {
        title() {
            return `${this.$locales.t('appName')} - ${this.$locales.t('live')}`;
        }
    },

    mounted(){
        this.GetLessons();
    },

    methods: {
        GetLessons(){
            this.HttpRequest({
                url: "api/live/listing",
                params: {
                    countryId: this.filter.countryId,
                    educationTypeId: this.filter.educationTypeId,
                    educationYearId: this.filter.educationYearId,
                    semesterId: this.filter.semesterId,
                    subjectId: this.filter.subjectId,
                    pageNo: this.pageNo,
                    pageSize: this.pageSize
                },
                auth: this.IsThereUser()
            }).then(this.GetSuccess)
            .catch(this.GetFailed);
        },
        GetSuccess(response){
            this.lessons = response.data.pageData;
            this.pageCount = Math.ceil(Math.max(response.data.count / Math.max(this.pageSize, 1), 1));
        },
        GetFailed(error){
            // error happend
        },
        PageChanged(page){
            this.pageNo = page;
            this.GetLessons();
        },
        ApplyFilter(filter){
            this.filter = filter;
            this.pageNo = 1;
            this.GetLessons();
        }
    },
}
</script>

<style scoped>
.min-height{
    min-height: 80vh;
}
</style>