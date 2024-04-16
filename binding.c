#include <inttypes.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#ifdef COMPILE_MODE
#include "cpuinfo.h"
#else
#include "build/cpuid/include/cpuinfo.h"
#endif


#ifdef _WIN32
#define DLL_EXPORT __declspec(dllexport) __cdecl
#else
#define DLL_EXPORT
#endif

#define API_EXPORT CPUINFO_ABI

#define FUNCTION_COPY(source, bool) \
    API_EXPORT bool binding_##source() { return source(); }

API_EXPORT int32_t binding_cpuinfo_get_name(uint8_t *name)
{
    if (name == NULL)
        return -1;
    strcpy(name, cpuinfo_get_processors()->package->name);
    return strlen(name);
}

API_EXPORT enum cpuinfo_vendor binding_cpuinfo_vendor()
{
    return cpuinfo_get_processors()->core->vendor;
}

API_EXPORT enum cpuinfo_uarch binding_cpuinfo_uarch()
{
    return cpuinfo_get_processors()->core->uarch;
}

API_EXPORT bool binding_cpuinfo_initialize()
{
    return cpuinfo_initialize();
}

API_EXPORT void binding_cpuinfo_deinilize()
{
    cpuinfo_deinitialize();
}

#define CACHE_INFO_API(native) \
    API_EXPORT struct cpuinfo_cache binding_##native() { struct cpuinfo_cache ret; memcpy(&ret,native(),sizeof(struct cpuinfo_cache)); }


CACHE_INFO_API(cpuinfo_get_l1i_caches);
CACHE_INFO_API(cpuinfo_get_l1d_caches);
CACHE_INFO_API(cpuinfo_get_l2_caches);
CACHE_INFO_API(cpuinfo_get_l3_caches);
CACHE_INFO_API(cpuinfo_get_l4_caches);