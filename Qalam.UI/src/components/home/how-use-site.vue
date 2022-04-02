<template>
    <div class="about">
        <v-container text-center fluid class="hide-background justify-center">
            <v-row no-gutters>
                <v-col md="6" cols="11" class="about-site about-site-title" :class="{right : $vuetify.rtl}" :style="`border-bottom: 75px solid ${$vuetify.theme.themes.light.primary}`">
                    <h2 class="how" style="color:white">
                        {{$locales.t('howToUse').replace(/{site-name}/g, $locales.t('appName'))}}
                    </h2>
                </v-col>
            </v-row>
            <v-row class="content" justify="center">
                <v-col md="4" cols="11" class="text-box-size">
                    <div>
                        <p>
                            في البداية هذا أول تعامل لي معكم ولن يكون الأخير بإذن الله صراحة قمة في الذوق وقمة في الإحترام والأهم النتيجة النهائية للعمل أكثر من رائعة وهذا هو الشيء المهم سلاسة في التعامل وصبر على كثرة الملاحظات والتعديلات كلمة حق تستحقون كل خير اتمنى لكم التوفيق
                        </p>
                    </div>
                </v-col>
                <v-col md="4" cols="11" class="text-box-size">
                    <div>
                        <p>
                            في البداية هذا أول تعامل لي معكم ولن يكون الأخير بإذن الله صراحة قمة في الذوق وقمة في الإحترام والأهم النتيجة النهائية للعمل أكثر من رائعة وهذا هو الشيء المهم سلاسة في التعامل وصبر على كثرة الملاحظات والتعديلات كلمة حق تستحقون كل خير اتمنى لكم التوفيق
                        </p>
                    </div>
                </v-col>
                <v-col md="4" cols="11" class="text-box-size">
                    <div>
                        <p>
                            في البداية هذا أول تعامل لي معكم ولن يكون الأخير بإذن الله صراحة قمة في الذوق وقمة في الإحترام والأهم النتيجة النهائية للعمل أكثر من رائعة وهذا هو الشيء المهم سلاسة في التعامل وصبر على كثرة الملاحظات والتعديلات كلمة حق تستحقون كل خير اتمنى لكم التوفيق
                        </p>
                    </div>
                </v-col>
            </v-row>
        </v-container>
    </div>
</template>

<script>
export default {
    name: "HowUseSite",
}
</script>

<style scoped>
.about-site{
    position: relative;
    top: -90px;
    border-right: 25px solid transparent;
	height: 0px;
}
.about-site-title:not(.right){
    left:-15px;
}
.right.about-site{
    border-right: initial;
    border-left: 25px solid transparent;
}
.right.about-site-title{
    right:-15px;
}

.about{
    text-align: right;
    background-color: #2d3e50; 
    background: url("~@/assets/images/bg.jpg") no-repeat center;
    background-size:cover;
    margin-top: 0px;
    margin-bottom: 50px;
}
.about h2 {
    font-size: 30px;
    padding-top: 18px;
}


.about p {
    padding-top: 15px;
    color: white;
    font-size: 20px;
    line-height: 35px;
    margin-bottom: 42px;
}

@media (max-width: 373px) {
    .about .how{
        padding-top: 0px;
        font-size:25px;
    }
}

.hide-background{
    padding:45px 0 20px;
    background:rgba(9,9,9,0.8);
    color:#DDD;
}

.content{
    padding-top: 35px;
    padding-bottom: 35px;
    margin: auto;
}

.text-box-size{
    max-width: 596px;
    min-height: 200px
}
</style>