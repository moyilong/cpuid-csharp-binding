#include <inttypes.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "cpuinfo.h"

#ifdef _WIN32
#define DLL_EXPORT __declspec(dllexport) __cdecl
#else
#define DLL_EXPORT
#endif

#define API_EXPORT DLL_EXPORT extern

#define FUNCTION_COPY(source, bool) \
    API_EXPORT bool binding_##source() { return source(); }

uint32_t API_EXPORT binding_cpuinfo_get_name(char *name)
{
    strcpy(name, cpuinfo_get_processors()->package->name);
    return strlen(name);
}

enum cpuinfo_vendor API_EXPORT binding_cpuinfo_vendor()
{
    return cpuinfo_get_processors()->core->vendor;
}

enum cpuinfo_uarch API_EXPORT binding_cpuinfo_uarch()
{
    return cpuinfo_get_processors()->core->uarch;
}

bool API_EXPORT binding_cpuinfo_initialize()
{
    return cpuinfo_initialize();
}

void API_EXPORT binding_cpuinfo_deinilize()
{
    cpuinfo_deinitialize();
}

void main()
{
    cpuinfo_initialize();
    printf("build: " __DATE__ " " __TIME__ "\n");
    char cpuname[64] = {0x00};
    int count = binding_cpuinfo_get_name(cpuname);
    printf("%d:%s\n", count, cpuname);
}