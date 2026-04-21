// ============ PAGINATION ============
export interface PaginationParams {
  pageNumber?: number;
  pageSize?: number;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  hasPrevious: boolean;
  hasNext: boolean;
}

// ============ ADMIN DASHBOARD ============
export interface AdminDashboardDto {
  date: string;
  branchSummaries: BranchSummaryDto[];
  totalPendingDemands: number;
  totalApprovedDemands: number;
  totalRejectedDemands: number;
  totalWasteToday: number;
  branchesOpenToday: number;
  branchesClosedToday: number;
}

export interface BranchSummaryDto {
  branchId: string;
  branchName: string;
  branchType: string;
  city: string;
  pendingDemandCount: number;
  approvedDemandCount: number;
  totalProductsInStock: number;
  totalWasteQuantity: number;
  isDayOpened: boolean;
  isDayClosed: boolean;
}

// ============ USER MANAGEMENT ============
export interface UserListDto {
  id: string;
  fullName: string;
  email: string;
  role: string;
  branchId: string | null;
  branchName: string | null;
  isActive: boolean;
  createdAt: string;
}

export interface UpdateUserDto {
  fullName: string;
  email: string;
  role: string;
  branchId: string | null;
  isActive: boolean;
}

export interface ResetPasswordDto {
  newPassword: string;
}

export interface UserFilterParams extends PaginationParams {
  role?: string;
  branchId?: string;
  search?: string;
}

// ============ CATEGORY MANAGEMENT ============
export interface CategoryListDto {
  id: string;
  name: string;
  productCount: number;
  sortOrder: number;
  createdAt: string;
}

export interface CreateCategoryDto {
  name: string;
  sortOrder: number;
}

export interface UpdateCategoryDto {
  name: string;
  sortOrder: number;
}

// ============ PRODUCT MANAGEMENT ============
export interface ProductListDto {
  id: string;
  name: string;
  categoryId: string;
  categoryName: string;
  productionBranchId: string | null;
  productionBranchName: string | null;
  unitType: string;
  unitPrice: number | null;
  isRawMaterial: boolean;
  sortOrder: number;
  createdAt: string;
}

export interface CreateProductDto {
  name: string;
  categoryId: string;
  productionBranchId: string | null;
  unitType: string;
  unitPrice: number | null;
  isRawMaterial: boolean;
  sortOrder: number;
}

export interface UpdateProductDto {
  name: string;
  categoryId: string;
  productionBranchId: string | null;
  unitType: string;
  unitPrice: number | null;
  isRawMaterial: boolean;
  sortOrder: number;
}

export interface ProductFilterParams extends PaginationParams {
  categoryId?: string;
  productionBranchId?: string;
  search?: string;
  unitType?: string;
}

// ============ BRANCH MANAGEMENT ============
export interface BranchListDto {
  id: string;
  name: string;
  city: string;
  branchType: string;
  pairedBranchId: string | null;
  pairedBranchName: string | null;
  isActive: boolean;
  userCount: number;
  createdAt: string;
}

export interface UpdateBranchDto {
  name: string;
  city: string;
  isActive: boolean;
}

// ============ DAY CLOSING CORRECTION ============
export interface DayClosingCorrectionDto {
  dayClosingDetailId: string;
  correctedEndOfDayCount: number;
  correctedCarryOverQuantity: number;
  correctionReason: string;
}

// ============ OUTLET CONTEXT ============
export interface AdminOutletContext {
  selectedBranchId: string | null;
  setSelectedBranchId: (branchId: string | null) => void;
}
