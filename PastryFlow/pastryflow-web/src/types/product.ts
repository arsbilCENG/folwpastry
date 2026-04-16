export interface Product {
  id: string;
  name: string;
  categoryId: string;
  categoryName: string;
  productionBranchId: string | null;
  productionBranchName: string | null;
  productType: number;
  productTypeName: string;
  unit: number;
  unitName: string;
  unitPrice: number | null;
  isActive: boolean;
}

export interface CategoryWithProducts {
  id: string;
  name: string;
  sortOrder: number;
  products: Product[];
}
