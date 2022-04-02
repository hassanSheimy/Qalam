<template>
    <v-card class="live-lesson-card ma-3 overflow-hidden word-break">
        <v-row no-gutters>
            <div class="ma-5">
                <div class="d-flex">
                    <h2 class="mx-3 primary--text headline">{{lesson.subjectName}}</h2>
                </div>
                <div class="d-block d-md-flex">
                    <div class="ma-3">
                        <v-icon color="secondary" size="16" class="mx-2">fas fa-book</v-icon>
                        <span>{{lesson.countryName}}</span>
                    </div>
                        
                    <div class="ma-3">
                        <v-icon color="secondary" size="16" class="mx-2">fas fa-book-open</v-icon>
                        <span>{{lesson.educationTypeName}}</span>
                    </div>
                </div>
            </div>
            <div class="ma-5">
                <div class="d-flex">
                    <h2 class="primary--text headline">{{lesson.teacherName}}</h2>
                    <v-chip v-if="!lesson.isLive" class="py-2 mx-4 hidden-sm-and-down" color="info darken-2" text-color="white" >
                        {{$locales.t('liveLesson')}}
                    </v-chip>
                    <v-chip v-else class="py-2 mx-4 hidden-sm-and-down" color="info darken-2" text-color="white" >
                        {{$locales.t('oldLesson')}}
                    </v-chip>
                </div>
                <div class="d-block d-md-flex">
                    <div class="ma-3">
                        <v-icon color="secondary" size="16" class="mx-2">fas fa-calendar-day</v-icon>
                        <span>{{lesson.educationYearName}}</span>
                    </div>
                    <div class="ma-3">
                        <v-icon color="secondary" size="16" class="mx-2">fas fa-calendar-day</v-icon>
                        <span>{{moment.utc(lesson.startDate).local().locale($locales.current).format("LLLL")}}</span>
                    </div>
                </div>
            </div>
            <v-spacer />
            <div class="my-5 mx-10 btn-index">
                <v-btn v-if="!reserved" :loading="loading" rounded outlined color="primary" @click="BookLive(true)">{{$locales.t('bookBtn')}}</v-btn>
                <v-btn v-if="reserved" :loading="loading" rounded outlined color="primary" @click="BookLive(false)">{{$locales.t('bookedBtn')}}</v-btn>
            </div>
        </v-row>
        <p v-if="lesson.isFree" class="ribbon ribbon-index" :class="{right: $vuetify.rtl}"><span>{{$locales.t('free')}}</span></p>
    </v-card>
</template>

<script>
import moment from 'moment'

export default {
    name: "LiveLessonCard",

    props: {
        lesson: {
            type: Object,
            required: true
        }
    },

    data: () => ({
        loading: false,
        loginDailog: false,
        moment: moment,
        isDelete: false
    }),

    computed: {
        reserved() {
            return this.lesson.followId != null;
        }
    },

    methods: {
        BookLive(state){
            if(this.IsThereUser()){
                this.canClick = false;
                this.isDelete = !state
                this.loading = true;
                this.HttpRequest({
                    url: 'api/live/follow/' + (!state ? this.lesson.followId : this.lesson.id),
                    method: !state ? 'DELETE' : 'POST',
                    auth: true,
                    loading: false,
                    faildNotify: true
                }).then(this.BookDone)
                .catch(this.BookFaild);
            }
            else{
                this.$root.$confirm(this.$locales.t('noLoginModal.title'), 
                this.$locales.t('noLoginModal.text'), {
                    okBtn: false
                });
            }
        },
        BookDone(response){
            this.loading = false;
            this.lesson.followId = (!this.isDelete ? response.data : null);
        },
        BookFaild(error){
            this.loading = false;
        }
    }
}
</script>

<style scoped>
.ribbon-index{
    z-index: 0;
}
.btn-index{
    z-index: 1;
}
</style>