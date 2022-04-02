<template>
    <v-container text-center class="features">
        <h1 v-html="$locales.t('whatSiteOffer').replace(/{site-name}/g, `<span class='primary--text mb-3'>${$locales.t('appName')}</span>`)" />
        <v-row justify="center">
            <v-col md="6" cols="12">
                <v-card class="bg-transparent">
                    <div class="feat">
                        <img src="@/assets/images/liveStream.png" width="200" height="200">
                        <h4>{{$locales.t('liveServiceTitle')}}</h4>
                        <p>{{$locales.t('liveServiceText')}}</p>
                        <v-btn to="/live" color="primary" large>{{$locales.t('bookLessonBtn')}}</v-btn>
                    </div>
                    <p class="ribbon ribbon-index" :class="{right: $vuetify.rtl}"><span>{{$locales.t('free')}}</span></p>
                </v-card>
            </v-col>
            <v-col md="6" cols="12">
                <v-card class="bg-transparent">
                    <div class="feat">
                        <img src="@/assets/images/exams.png" width="200" height="200">
                        <h4>{{$locales.t('examServiceTitle')}}</h4>
                        <p>{{$locales.t('examServiceText')}}</p>
                        <v-btn color="primary" disabled large @click="dialog = true">{{$locales.t('startExamBtn')}}</v-btn>
                    </div>
                    <p class="ribbon ribbon-index" :class="{right: $vuetify.rtl}"><span>{{$locales.t('soonBadge')}}</span></p>
                </v-card>
            </v-col>
            <exam-selector :dialog="dialog" @close="ModalClosed" @done="ModalDone" />
        </v-row>
    </v-container>
</template>

<script>
import ExamSelector from '@/components/modals/exam-selector.vue'

export default {
    name: "SiteServices",

    components: {
        ExamSelector
    },

    data: () => ({
        dialog: false,
    }),

    methods: {
        ModalClosed() {
            this.dialog = false
        },
        ModalDone() {
            this.dialog = false
        }
    },
}
</script>

<style scoped>
.features {
	padding-bottom:50px;
	padding-top:50px;
}
.features h1{
	padding-bottom:30px;
}
.features .feat p {
	line-height:2em;
    padding-left:10px;
    min-height: 100px
}
.features .feat  {
	padding-bottom:30px;
}

.ribbon-index{
    z-index: 0;
}

.bg-transparent{
    background-color: transparent;
}
</style>