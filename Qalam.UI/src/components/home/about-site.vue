<template>
    <v-container text-center fluid class="about">
        <v-row no-gutters>
            <v-col md="5" cols="12">
                <div class="about-site about-site-title" :class="{right : $vuetify.rtl}" :style="`border-bottom: 75px solid ${$vuetify.theme.themes.light.primary}`">
                    <h2 class="how" style="color:white">
                        {{$locales.t('whatIsSite').replace(/{site-name}/g, $locales.t('appName'))}}
                    </h2>
                </div>
            </v-col>
        </v-row>
        <v-row>
            <v-col v-for="(box, index) in boxes" :key="index" md="4" cols="11">
                <div>
                    <img :src="box.image" :alt="box.alt" width="200" height="200">
                    <h1 style="color: white">{{box.title}}</h1>
                    <p v-html="box.text.replace(/{site-name}/g, `<span class='primary--text mb-3'>${$locales.t('appName')}</span>`)" />
                </div>
            </v-col>
            <v-col v-if="!IsThereUser()" cols="12">
                <v-btn color="primary" large to="/signup">{{$locales.t('startForFreeBtn')}}</v-btn>
            </v-col>
        </v-row>
    </v-container>
</template>

<script>
import device from '@/assets/images/devices.png'
import classroom from '@/assets/images/classroom.png'
import winner from '@/assets/images/winner.png'

export default {
    name: "AboutSite",

    computed:{
        boxes() {
            return [
                {
                    image: device,
                    alt: 'access any device',
                    title: this.$locales.t('learnAnyWhereTitle'),
                    text: this.$locales.t('learnAnyWhereText')
                },
                {
                    image: classroom,
                    alt: 'class room',
                    title: this.$locales.t('specialTeachersTitle'),
                    text: this.$locales.t('specialTeachersText')
                },
                {
                    image: winner,
                    alt: 'contest and exams',
                    title: this.$locales.t('contestsAndExamsTitle'),
                    text: this.$locales.t('contestsAndExamsText')
                }
            ]
        }
    },
}
</script>

<style scoped>
.about-site:not(.right){
    position: relative;
    top: -60px;
    border-right: 25px solid transparent;
	height: 0px;
}
.about-site-title:not(.right){
    left:-15px;
}
.right.about-site{
    position: relative;
    top: -60px;
    border-left: 25px solid transparent;
	height: 0px;
}
.right.about-site-title{
    right:-15px;
}

.about{
    text-align: right; 
    background: url("~@/assets/images/aboutSection.jpg") no-repeat center;
    background-size:cover;
    margin-top: 100px;
}
.about h2 {
    font-size: 30px;
    padding-top: 22px;
}

@media (max-width: 373px) {
    .about .how{
        padding-top: 0px;
        font-size:25px;
    }
}

.about p {
    padding-top: 15px;
    color: white;
    font-size: 20px;
    line-height: 35px;
    margin-bottom: 42px;
}
</style>