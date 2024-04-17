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

#define STRUCT_API_COPY(native,type) \
    API_EXPORT type binding_##native() { type ret; memcpy(&ret,native(),sizeof(type)); }

#define CPUINFO_VERSION_STR "Binding " RID_NAME " " __DATE__ " " __TIME__ " libcpuinfo: git+" CPUINFO_VERSION

API_EXPORT int32_t binding_get_version(uint8_t* version) {
	if (version == NULL)
		return -1;
	strcpy(version, CPUINFO_VERSION_STR);
	return strlen(version);
}

API_EXPORT int32_t binding_cpuinfo_get_name(uint8_t* name)
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

STRUCT_API_COPY(cpuinfo_get_l1i_caches, struct cpuinfo_cache);
STRUCT_API_COPY(cpuinfo_get_l1d_caches, struct cpuinfo_cache);
STRUCT_API_COPY(cpuinfo_get_l2_caches, struct cpuinfo_cache);
STRUCT_API_COPY(cpuinfo_get_l3_caches, struct cpuinfo_cache);
STRUCT_API_COPY(cpuinfo_get_l4_caches, struct cpuinfo_cache);