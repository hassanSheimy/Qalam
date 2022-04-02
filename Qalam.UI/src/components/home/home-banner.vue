<template>
    <section class="home-banner">
        <v-carousel cycle height="800" hide-delimiters :show-arrows="false">
            <v-carousel-item>
                <div class="image-container">
                    <img src="@/assets/images/header-image-1.jpeg" class="d-block w-100" style="max-width:120%" height="800" />
                    <div class="caption-centered" style="font-size: 22px;color:white">
                        <p v-html="$locales.t('welcomInQalam').replace(/{site-name}/g, `<span class='primary--text mb-3' style='font-size: 26px; font-weight: bold'>${$locales.t('appName')}</span>`)"/>
                        <p v-html="$locales.t('welcomeMessage').replace(/{site-name}/g, `<span class='primary--text mb-3'>${$locales.t('appName')}</span>`)"/>
                    </div>
                </div>
            </v-carousel-item>
            <v-carousel-item>
                <div class="image-container">
                    <img src="@/assets/images/header-image-2.png" class="d-block w-100" style="max-width:100%" height="800" />
                    <div class="caption-centered" style="font-size: 22px;color:white">
                        <p v-html="$locales.t('welcomInQalam').replace(/{site-name}/g, `<span class='primary--text mb-3' style='font-size: 26px; font-weight: bold'>${$locales.t('appName')}</span>`)"/>
                        <p v-html="$locales.t('welcomeMessage').replace(/{site-name}/g, `<span class='primary--text mb-3'>${$locales.t('appName')}</span>`)"/>
                    </div>
                </div>
            </v-carousel-item>
        </v-carousel>
    </section>
</template>

<script>
export default {
    name: "HomeBanner"
}
</script>

<style scoped>
.image-container {
    position: relative;
    text-align: center;
    color: white;
    padding: 0;
    margin: 0;
}
.caption-centered {
    background-color: rgba(0,0,0,0.6);
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
}
.w-100{
    width: 100%!important;
}
</style>