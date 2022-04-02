<template>
    <div class="home">
        <default-header />
        <home-banner />
        <about-site />
        <site-services />
        <how-use-site />
        <access-anywhere />
        <default-footer />
    </div>
</template>

<script>
// @ is an alias to /src
import DefaultHeader from '@/components/layout/default-header.vue'
import HomeBanner from '@/components/home/home-banner.vue'
import AboutSite from '@/components/home/about-site.vue';
import SiteServices from '@/components/home/site-services.vue'
import HowUseSite from '@/components/home/how-use-site.vue'
import AccessAnywhere from '@/components/home/access-anywhere.vue';
import DefaultFooter from '@/components/layout/default-footer.vue';

export default {
    name: 'home',

    components: {
        DefaultHeader,
        HomeBanner,
        AboutSite,
        SiteServices,
        HowUseSite,
        AccessAnywhere,
        DefaultFooter
    },

    data: () => ({
        dialog: false
    })
}
</script>
