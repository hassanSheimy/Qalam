<template>
    <div>
        <default-header />
        <v-container fluid>
            <v-row class="mt-4" no-gutters>
                <video controls autoplay loop controlsList="nodownload" class="full-width">
                    <source src="@/assets/dummy-video.mp4" type="video/mp4" />
                </video>
            </v-row>
        </v-container>
        <secondary-footer />
    </div>
</template>

<script>
import DefaultHeader from '@/components/layout/default-header.vue'
import SecondaryFooter from '@/components/layout/secondary-footer.vue'

export default {
    name: "LessonVideo",

    components: {
        DefaultHeader,
        SecondaryFooter
    },

    mounted() {
        if(!this.CheckAuthority()){
            this.Unauthorized(this.$route.fullPath);
        }
    },

    methods: {
        CheckAuthority() {
            return true;
        }
    }
}
</script>

<style scoped>
.full-width{
    width: 100%;
    height: 100%;
}
</style>